using Microsoft.EntityFrameworkCore;
using MKExpress.API.Constants;
using MKExpress.API.Data;
using MKExpress.API.Dto.Response;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories
{
    public class CustomerAccountStatementRespository : ICustomerAccountStatementRespository
    {
        private readonly MKExpressDbContext _context;
        public CustomerAccountStatementRespository(MKExpressDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAdvancePayment(List<CustomerAccountStatement> customerAccountStatements)
        {
            var order = await _context.Orders
                .Include(x=>x.AccountStatements)
                .Where(x => !x.IsCancelled && !x.IsDeleted && x.Id == customerAccountStatements.First().OrderId)
                .FirstOrDefaultAsync();
            if (order != null && customerAccountStatements != null)
            {
                for (int i = 0; i < customerAccountStatements.Count; i++)
                {
                    if (customerAccountStatements[i].Reason==AccountStatementReasonEnum.OrderCreated.ToString())
                    {
                        customerAccountStatements[i].Balance =order.TotalAmount;
                    }
                    else
                    {
                        bool hasFirstAdvance = order.AccountStatements.Where(x => x.IsFirstAdvance).Count() > 0;
                        if (customerAccountStatements.Count == 1 && !hasFirstAdvance)
                        {
                            customerAccountStatements[i].Balance = (decimal)(order.TotalAmount - customerAccountStatements[i].Credit);
                            customerAccountStatements[i].IsFirstAdvance = true;
                        }
                        else if(customerAccountStatements.Count == 1)
                        {
                            customerAccountStatements[i].Balance = order.BalanceAmount - customerAccountStatements[i].Credit ?? 0;
                        }
                        else
                        {
                            customerAccountStatements[i].Balance = customerAccountStatements[i - 1].Balance - customerAccountStatements[i].Credit ?? 0;
                        }
                    }
                }
                _context.CustomerAccountStatements.AddRange(customerAccountStatements);
                return await _context.SaveChangesAsync();
            }
            return default;
        }

        public async Task<List<CustomerAccountStatement>> GetAdvancePaymentStatement(int orderId)
        {
            return await _context.CustomerAccountStatements
                .Where(x => !x.IsDeleted && (x.Reason == AccountStatementReasonEnum.PaymentReceived.ToString() || x.Reason == AccountStatementReasonEnum.AdvancedPaid.ToString()) && x.OrderId == orderId)
                .OrderByDescending(x => x.PaymentDate.Date)
                .ThenByDescending(x => x.Id)
                .ToListAsync();
        }

        public async Task<decimal> GetCustomerBalanceAmount(int customerId, int excludeOrderId)
        {
            var customer = await _context.Customers.Where(x => !x.IsDeleted && x.Id == customerId).FirstOrDefaultAsync();
            if (customer == null)
            {
                throw new BusinessRuleViolationException(StaticValues.CustomerNotFoundError, StaticValues.CustomerNotFoundMessage);
            }

            var customerStatement = await _context.CustomerAccountStatements
                .Include(x => x.Customer)
                .Where(x => (excludeOrderId == 0 || x.OrderId != excludeOrderId) && (x.Customer.Contact1 == customer.Contact1 || x.Customer.Contact2 == customer.Contact1))
                .ToListAsync();
            if (customerStatement == null)
                return default;
            var totalCredit = customerStatement.Sum(x => x.Credit);
            var totalDebit = customerStatement.Sum(x => x.Debit);
            return (decimal)(totalDebit - totalCredit);
        }
        public async Task<List<CustomerPreviousAmountStatementResponse>> GetCustomerPreviousAmountStatement(string contactNo)
        {
            var customerIds = await _context.Customers
                                                .Where(x => !x.IsDeleted && x.Contact1 == contactNo)
                                                .Select(x => x.Id)
                                                .ToListAsync();
            var statements = await _context.CustomerAccountStatements
                .Include(x => x.Order)
                .Include(x => x.Customer)
                .Where(x => !x.IsDeleted && customerIds.Contains(x.CustomerId) && !x.Order.IsCancelled && !x.Order.IsDeleted).ToListAsync();
            var statementGroup = statements.GroupBy(x => x.OrderId).ToList();
            var responses = new List<CustomerPreviousAmountStatementResponse>();
            foreach (IGrouping<int, CustomerAccountStatement> item in statementGroup)
            {
                var orderAmount = item.FirstOrDefault().Order.TotalAmount;
                var PaidAmount = item.Where(x => x.Reason == AccountStatementReasonEnum.PaymentReceived.ToString()).Sum(x => x.Credit);
                var advanceAmount = (decimal)item.Where(x => x.Reason == AccountStatementReasonEnum.AdvancedPaid.ToString()).Sum(x => x.Credit);
                responses.Add(new CustomerPreviousAmountStatementResponse()
                {
                    OrderNo = item.FirstOrDefault().Order.OrderNo,
                    AdvanceAmount = advanceAmount,
                    TotalAmount = orderAmount,
                    PaidAmount = (decimal)PaidAmount,
                    BalanceAmount = orderAmount - PaidAmount - advanceAmount
                });
            }
            return responses;
        }

        public async Task<List<CustomerAccountStatement>> GetPaymentSummary(DateTime fromDate, DateTime toDate, string paymentType, string paymentMode)
        {
            try
            {
                string accountStatementReasonEnum = paymentType.ToLower() == StaticValues.TextDeliveryLower ? AccountStatementReasonEnum.PaymentReceived.ToString() : AccountStatementReasonEnum.AdvancedPaid.ToString();

                paymentType = string.IsNullOrEmpty(paymentType) ? StaticValues.TextAdvanceLower : paymentType.ToLower();
                paymentMode = string.IsNullOrEmpty(paymentMode) ? PaymentModeEnum.Cash.ToString().ToLower() : paymentMode.ToLower();
                List<CustomerAccountStatement> filterdateData = new List<CustomerAccountStatement>();
                if (paymentType == StaticValues.TextAdvanceLower)
                {
                    var data = await _context.Orders
                        .Include(x=>x.OrderDetails)
                      .Include(x => x.AccountStatements.Where(y => y.IsFirstAdvance == true && !y.IsDeleted && y.Reason == AccountStatementReasonEnum.AdvancedPaid.ToString()))
                      .Include(x => x.Customer)
                       .Where(x => !x.IsDeleted &&
                                   x.OrderDate.Date >= fromDate.Date &&
                                   x.OrderDate.Date <= toDate.Date &&
                                   (paymentMode == StaticValues.TextAllLower || x.PaymentMode == paymentMode))
                       .OrderByDescending(x => x.OrderNo)
                       .ToListAsync();
                    filterdateData=data.Select(x => new CustomerAccountStatement()
                       {
                           Order = x,
                           Balance = x.AccountStatements.FirstOrDefault()?.Balance??0,
                           Credit = x.AccountStatements.FirstOrDefault()?.Credit??0,
                           DeliveredQty = x.AccountStatements.FirstOrDefault()?.DeliveredQty??0,
                           Id = x.AccountStatements.FirstOrDefault()?.Id??0,
                           PaymentDate = x.AccountStatements.FirstOrDefault()?.PaymentDate??DateTime.MinValue,
                           PaymentMode = x.AccountStatements.FirstOrDefault()?.PaymentMode??StaticValues.TextNoPayment,
                           Reason = x.AccountStatements.FirstOrDefault()?.Reason ?? string.Empty,
                           Remark = x.AccountStatements.FirstOrDefault()?.Remark ?? string.Empty
                    })
                       .ToList();
                }
                else if (paymentType == StaticValues.TextDeliveryLower)
                {
                    filterdateData = await _context.CustomerAccountStatements
                      .Include(x => x.Order)
                      .Include(x => x.Customer)
                       .Where(x => !x.IsDeleted &&
                       !x.Order.IsDeleted && !x.Order.IsCancelled &&
                                   x.PaymentDate.Date >= fromDate &&
                                   x.PaymentDate.Date <= toDate &&
                                   !x.IsFirstAdvance &&
                                   (x.Reason == AccountStatementReasonEnum.AdvancedPaid.ToString() || x.Reason == AccountStatementReasonEnum.PaymentReceived.ToString()) &&
                                   (paymentMode == StaticValues.TextAllLower || x.PaymentMode == paymentMode))
                       .OrderByDescending(x => x.Order.OrderNo)
                       .ToListAsync();
                }



                //data.ForEach(res =>
                //{
                //  var temp=  res.AccountStatements.Where(y => y.PaymentDate.Date == res.OrderDate.Date &&
                //                 ((paymentType == "delivery" &&
                //                 y.Reason == AccountStatementReasonEnum.PaymentReceived.ToString()
                //                 ) ||
                //                 (paymentType == StaticValues.TextAdvanceLower &&
                //                 (y.Reason == AccountStatementReasonEnum.AdvancedPaid.ToString() ||
                //                 y.Reason == AccountStatementReasonEnum.OrderCreated.ToString()))
                //                 )).Select(x => new CustomerAccountStatement()
                //                 {
                //                     Order = res,
                //                     Balance = x.Balance,
                //                     Credit = x.Credit,
                //                     PaymentMode = x.Credit == null ? "No Payment" : x.PaymentMode,
                //                     Id = x.Id,
                //                     PaymentDate = x.PaymentDate,
                //                 }).OrderByDescending(x=>x.Credit).FirstOrDefault();
                //    if(temp!=null && filterdateData.Where(x=>x.Order.OrderNo==res.OrderNo).Count()==0)
                //    {
                //        filterdateData.Add(temp);
                //    }
                //});
                return filterdateData;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> UpdateAdvanceDate(int orderId, DateTime advanceDate)
        {
            var oldData = await _context.CustomerAccountStatements.Where(x => !x.IsDeleted && x.OrderId == orderId && x.Reason == AccountStatementReasonEnum.AdvancedPaid.ToString()).FirstOrDefaultAsync();
            if (oldData == null)
                return -1;// Data not Found
            oldData.PaymentDate = advanceDate;
            var entity = _context.CustomerAccountStatements.Attach(oldData);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();

        }

        public async Task<int> UpdateStatement(CustomerAccountStatement accountStatement)
        {
            var oldStatement = await _context.CustomerAccountStatements
                                                .Where(x => x.Id == accountStatement.Id && !x.IsDeleted)
                                                .FirstOrDefaultAsync();
            if(oldStatement==null)
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            if(accountStatement.PaymentDate.Date>DateTime.Today.Date)
                throw new BusinessRuleViolationException(StaticValues.FutureDateError, StaticValues.FutureDateMessage);
            oldStatement.PaymentDate = accountStatement.PaymentDate;
            oldStatement.Remark = oldStatement.Remark + " || Payment Info Updated on : " + DateTime.Now;
            oldStatement.PaymentMode = accountStatement.PaymentMode;
            var entity = _context.Attach(oldStatement);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
    }
}
