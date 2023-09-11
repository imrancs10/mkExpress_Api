using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using MKExpress.API.Constants;
using MKExpress.API.Data;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Request.Orders;
using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Orders;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MKExpressDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IWorkTypeStatusRepository _workTypeStatusRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly ICustomerAccountStatementRespository _customerAccountStatementRespository;
        private readonly decimal VAT = 0;
        public OrderRepository(MKExpressDbContext context,
            IConfiguration configuration,
            IWorkTypeStatusRepository workTypeStatusRepository,
            IExpenseRepository expenseRepository,
            ICustomerAccountStatementRespository customerAccountStatementRespository)
        {
            _context = context;
            _configuration = configuration;
            _workTypeStatusRepository = workTypeStatusRepository;
            _expenseRepository = expenseRepository;
            _customerAccountStatementRespository = customerAccountStatementRespository;
            VAT = _configuration.GetValue<decimal>("Vat");
        }

        public async Task<Order> Add(Order order)
        {
            try
            {

                var entity = _context.Orders.Attach(order);
                entity.State = EntityState.Added;
                var trans = _context.Database.BeginTransaction();
                if (await _context.SaveChangesAsync() > 0)
                {
                    List<CustomerAccountStatement> customerAccountStatements = new List<CustomerAccountStatement>();
                    CustomerAccountStatement accountStatement = new CustomerAccountStatement()
                    {
                        CustomerId = order.CustomerId,
                        OrderId = entity.Entity.Id,
                        Debit = order.TotalAmount,
                        PaymentMode = order.PaymentMode,
                        Remark = $"Order No- {order.OrderNo}",
                        PaymentDate = order.OrderDate,
                        Balance = order.TotalAmount,
                        Credit = 0,
                        DeliveredQty = 0,
                        Reason = AccountStatementReasonEnum.OrderCreated.ToString()
                    };
                    customerAccountStatements.Add(accountStatement);
                    if (entity.Entity.AdvanceAmount > 0)
                    {
                        var advanceAccountStatement = new CustomerAccountStatement()
                        {
                            CustomerId = order.CustomerId,
                            OrderId = entity.Entity.Id,
                            Credit = order.AdvanceAmount,
                            Debit = 0,
                            PaymentMode = order.PaymentMode,
                            Remark = $"Order No- {order.OrderNo}",
                            PaymentDate = order.OrderDate,
                            DeliveredQty = 0,
                            Balance = order.TotalAmount - order.AdvanceAmount,
                            Reason = AccountStatementReasonEnum.AdvancedPaid.ToString(),
                            IsFirstAdvance = true
                        };
                        customerAccountStatements.Add(advanceAccountStatement);
                    }
                    var result = await _customerAccountStatementRespository.AddAdvancePayment(customerAccountStatements);
                    if (result > 0)
                    {
                        trans.Commit();
                        return entity.Entity;
                    }
                    else
                    {
                        trans.Rollback();
                        return default;
                    }
                }
                return default;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OrderDetail> CancelOrderDetail(int orderDetailId, string note)
        {
            var oldOrderDetail = await _context.OrderDetails.Include(x => x.Order).Where(x => x.Id == orderDetailId && !x.IsDeleted).FirstOrDefaultAsync();
            if (oldOrderDetail == null)
                return default;

            if (oldOrderDetail.Status != OrderStatusEnum.Active.ToString())
                throw new BusinessRuleViolationException(StaticValues.OrderInProcessingError, StaticValues.OrderInProcessingMessage);

            if (oldOrderDetail.IsCancelled)
                throw new BusinessRuleViolationException(StaticValues.OrderAlreadyCancelledError, StaticValues.OrderAlreadyCancelledMessage);

            oldOrderDetail.IsCancelled = true;
            oldOrderDetail.Note = note;
            oldOrderDetail.CancelledDate = DateTime.Now;
            oldOrderDetail.Status = OrderStatusEnum.Cancelled.ToString();
            //Change order Amount
            decimal amountWithVAT = oldOrderDetail.Price + (oldOrderDetail.Price / 100 * VAT);
            oldOrderDetail.Order.SubTotalAmount -= oldOrderDetail.Price;
            oldOrderDetail.Order.TotalAmount -= amountWithVAT;
            oldOrderDetail.Order.BalanceAmount -= amountWithVAT;
            oldOrderDetail.Order.Qty -= 1;
            //
            var entity = _context.OrderDetails.Attach(oldOrderDetail);
            entity.State = EntityState.Modified;

            if (await _context.SaveChangesAsync() > 0)
            {
                var accountStatement = new CustomerAccountStatement()
                {
                    CustomerId = oldOrderDetail.Order.CustomerId,
                    OrderId = oldOrderDetail.Order.Id,
                    OrderDetailId = orderDetailId,
                    Credit = oldOrderDetail.TotalAmount,
                    Remark = $"Order No- {oldOrderDetail.OrderNo}",
                    PaymentDate = DateTime.Now.Date,
                    PaymentMode = PaymentModeEnum.Cash.ToString(),
                    Reason = AccountStatementReasonEnum.SubOrderCancelled.ToString()
                };
                _context.CustomerAccountStatements.Add(accountStatement);
                await _context.SaveChangesAsync();
            }
            var oldOrder = oldOrderDetail.Order;


            //Check if all sub orders cancelled
            var orderDetails = await _context.OrderDetails.Where(x => x.OrderId == oldOrder.Id && !x.IsDeleted).ToListAsync();
            if (orderDetails.Where(x => x.IsCancelled).Count() == orderDetails.Count())
            {
                oldOrder.IsCancelled = true;
                oldOrder.CancelledDate = DateTime.Now;
            }
            oldOrder.CancelledAmount += oldOrderDetail.TotalAmount;
            var entityOrder = _context.Orders.Attach(oldOrder);
            entityOrder.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldOrderDetail;
        }

        public async Task<Order> CancelOrder(int orderId, string note)
        {
            var oldOrder = await _context.Orders.Include(x => x.OrderDetails).Where(x => x.Id == orderId).FirstOrDefaultAsync();
            if (oldOrder == null)
                return default;

            if (oldOrder.Status != OrderStatusEnum.Active.ToString())
                throw new BusinessRuleViolationException(StaticValues.OrderInProcessingError, StaticValues.OrderInProcessingMessage);

            if (oldOrder.IsCancelled)
            {
                throw new BusinessRuleViolationException(StaticValues.OrderAlreadyCancelledError, StaticValues.OrderAlreadyCancelledMessage);
            }
            var trans = await _context.Database.BeginTransactionAsync();
            oldOrder.IsCancelled = true;
            oldOrder.CancelledDate = DateTime.Now;
            oldOrder.Note = note;
            var entity = _context.Orders.Attach(oldOrder);
            string allOrderNo = string.Empty;
            foreach (OrderDetail orderDetail in oldOrder.OrderDetails)
            {
                orderDetail.IsCancelled = true;
                orderDetail.CancelledDate = DateTime.Now;
                orderDetail.Status = OrderStatusEnum.Cancelled.ToString();
                orderDetail.Note = note;
                allOrderNo = $"{orderDetail.OrderNo},";
            }
            entity.State = EntityState.Modified;
            if (await _context.SaveChangesAsync() > 0)
            {
                var accountStatement = new CustomerAccountStatement()
                {
                    CustomerId = oldOrder.CustomerId,
                    OrderId = orderId,
                    PaymentMode = PaymentModeEnum.Cash.ToString(),
                    Credit = oldOrder.OrderDetails.Where(x => !x.IsDeleted).Sum(x => x.TotalAmount),
                    Remark = $"Order No-{allOrderNo}{oldOrder.OrderNo}",
                    Reason = AccountStatementReasonEnum.OrderCancelled.ToString(),
                    PaymentDate = DateTime.Now.Date
                };
                _context.CustomerAccountStatements.Add(accountStatement);
                if (await _context.SaveChangesAsync() > 0)
                {
                    var expenseResult = await _expenseRepository.AddCancelOrderExpense(oldOrder.AdvanceAmount, "Cancelled Order No. " + oldOrder.OrderNo);
                    if (expenseResult > 0)
                    {
                        await trans.CommitAsync();
                        return oldOrder;
                    }
                    await trans.RollbackAsync();
                    return new Order();
                }
                else
                {
                    await trans.RollbackAsync();
                    return new Order();
                }
            }
            return default;
        }

        public async Task<int> Delete(int orderId)
        {
            Order oldOrder = await _context.Orders
              .Include(x => x.AccountStatements)
              .Include(x => x.OrderDetails)
              .Where(order => order.Id == orderId)
              .FirstOrDefaultAsync();

            if (oldOrder == null)
                throw new BusinessRuleViolationException(StaticValues.OrderNotFoundError, StaticValues.OrderNotFoundMessage);

            if (oldOrder.IsDeleted)
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyDeletedError, StaticValues.RecordAlreadyDeletedMessage);

            if (oldOrder.Status != OrderStatusEnum.Active.ToString())
                throw new BusinessRuleViolationException(StaticValues.OrderInProcessingError, StaticValues.OrderInProcessingMessage);

            if (oldOrder.IsCancelled)
                throw new BusinessRuleViolationException(StaticValues.OrderAlreadyCancelledError, StaticValues.OrderAlreadyCancelledMessage);

            if (oldOrder.OrderDetails.Count(x => x.IsCancelled) > 0)
                throw new BusinessRuleViolationException(StaticValues.OrderPartiallyCancelledError, StaticValues.OrderPartiallyCancelledMessage);
            var trans = _context.Database.BeginTransaction();
            oldOrder.IsDeleted = true;
            oldOrder.DeletedDate = DateTime.Now;
            oldOrder.Status = OrderStatusEnum.Deleted.ToString();
            EntityEntry<Order> entity = _context.Orders.Update(oldOrder);
            entity.State = EntityState.Modified;

            if (await _context.SaveChangesAsync() > 0)
            {
                string allOrderNo = string.Empty;
                var orderDetailIds = oldOrder.OrderDetails.Select(x => x.Id).ToList();
                foreach (OrderDetail orderDetail in oldOrder.OrderDetails)
                {
                    orderDetail.IsDeleted = true;
                    orderDetail.Status = OrderStatusEnum.Deleted.ToString();
                    orderDetail.Note = "Order Deleted";
                    allOrderNo = $"{orderDetail.OrderNo},";
                }
                var accountStatement = new CustomerAccountStatement()
                {
                    CustomerId = oldOrder.CustomerId,
                    OrderId = orderId,
                    Credit = oldOrder.BalanceAmount,
                    Remark = $"Order No-{allOrderNo}{oldOrder.OrderNo}",
                    PaymentDate = DateTime.Now.Date,
                    PaymentMode = PaymentModeEnum.Cash.ToString(),
                    Reason = AccountStatementReasonEnum.OrderDeleted.ToString()
                };
                _context.CustomerAccountStatements.Add(accountStatement);
                var result= await _context.SaveChangesAsync();
                if(result>0)
                {
                   var workResult=await _workTypeStatusRepository.DeleteByOrderDetailId(orderDetailIds);
                    if(workResult>0)
                    {
                        trans.Commit();
                        return workResult;
                    }    
                }
            }
            trans.Rollback();
            return default;
        }

        public async Task<Order> Get(int orderId)
        {
            return await _context.Orders
                         .Include(x => x.Customer)
                        .Include(x => x.Employee)
                        .Include(x => x.AccountStatements)
                        .Include(x => x.EmployeeCreated)
                        .Include(x => x.EmployeeUpdated)
                        .Include(x => x.CancelledOrder)
                        .Include(x => x.OrderDetails.Where(y => !y.IsCancelled && !y.IsDeleted))
                        .ThenInclude(x => x.DesignSample)
                        .ThenInclude(x => x.MasterDesignCategory)
                        .Where(order => order.Id == orderId)
                        .AsNoTracking()
                        .FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<Order>> GetAll(PagingRequest pagingRequest)
        {
            var data = await _context
                .Orders
                .Include(x => x.Customer)
                .Include(x => x.Employee)
                .Include(x => x.EmployeeCreated)
                .Include(x => x.EmployeeUpdated)
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.DesignSample)
                .ThenInclude(x => x.MasterDesignCategory)
                .Where(x => !x.IsCancelled &&
                        !x.IsDeleted &&
                        x.OrderDate.Date >= pagingRequest.FromDate.Date &&
                        x.OrderDate.Date <= pagingRequest.ToDate.Date
                      )
                .OrderByDescending(x => x.OrderNo)
                .Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1))
                .Take(pagingRequest.PageSize)
                .ToListAsync();

            PagingResponse<Order> pagingResponse = new PagingResponse<Order>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data,
                TotalRecords = await _context.Orders.Where(x => !x.IsCancelled &&
                        !x.IsDeleted &&
                        x.OrderDate.Date >= pagingRequest.FromDate.Date &&
                        x.OrderDate.Date <= pagingRequest.ToDate.Date
                      ).CountAsync()
            };
            return pagingResponse;
        }

        public async Task<int> GetOrderNo()
        {
            int defaultOrderNo = _configuration.GetValue<int>("OrderNoStartFrom");
            var order = await _context.Orders.OrderByDescending(x => x.OrderNo).FirstOrDefaultAsync();
            return order == null ? defaultOrderNo : int.Parse(order.OrderNo) + 1;
        }

        public async Task<PagingResponse<Order>> Search(SearchPagingRequest searchPagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.Orders
                .Include(x => x.Customer)
                .Include(x => x.Employee)
                .Include(x => x.EmployeeCreated)
                .Include(x => x.EmployeeUpdated)
                .Include(x => x.CancelledOrder)
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.DesignSample)
                .ThenInclude(x => x.MasterDesignCategory)
                .Where(order => !order.IsDeleted &&
                                !order.IsCancelled &&
                                (
                                    searchTerm.Equals(string.Empty) ||
                                    order.PaymentMode.Equals(searchTerm) ||
                                    (order.Customer.Firstname + " " + order.Customer.Lastname).Contains(searchTerm) ||
                                    (order.EmployeeCreated.FirstName + " " + order.EmployeeCreated.LastName).Contains(searchTerm) ||
                                     (searchPagingRequest.isAdmin && (order.Employee.FirstName + " " + order.Employee.LastName).Contains(searchTerm)) ||
                                    order.City.Contains(searchTerm) ||
                                    order.OrderDetails.Any(x => x.OrderNo.Contains(searchTerm)) ||
                                    order.Customer.Contact1.Contains(searchTerm) ||
                                     order.Customer.Contact2.Contains(searchTerm) ||
                                    order.CustomerRefName.Contains(searchTerm) ||
                                    order.Status.Contains(searchTerm) ||
                                    order.OrderNo.Contains(searchTerm))
                                )
                .OrderByDescending(x => x.OrderNo)
                .ToListAsync();
            PagingResponse<Order> pagingResponse = new PagingResponse<Order>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<Order> Update(Order order)
        {
            EntityEntry<Order> oldOrder = _context.Update(order);
            oldOrder.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldOrder.Entity;
        }

        public async Task<PagingResponse<Order>> GetCancelledOrders(PagingRequestByWorkType pagingRequest)
        {
            var data = await _context
                .Orders
               .Include(x => x.Customer)
                .Include(x => x.Employee)
                .Include(x => x.EmployeeCreated)
                .Include(x => x.EmployeeUpdated)
                .Include(x => x.OrderDetails.Where(y => y.IsCancelled))
                .ThenInclude(x => x.DesignSample)
                .ThenInclude(x => x.MasterDesignCategory)
                .Where(x => x.OrderDetails.Where(y => y.IsCancelled &&
                 (pagingRequest.SalesmanId == 0 || x.EmployeeId == pagingRequest.SalesmanId) &&
                            y.CancelledDate.Date >= pagingRequest.FromDate.Date &&
                            y.CancelledDate.Date <= pagingRequest.ToDate.Date).Count() > 0)
                .OrderByDescending(x => x.OrderNo)
                .ToListAsync();

            PagingResponse<Order> pagingResponse = new PagingResponse<Order>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<PagingResponse<Order>> GetDeletedOrders(PagingRequestByWorkType pagingRequest)
        {
            var data = await _context
                 .Orders
                .Include(x => x.Customer)
                 .Include(x => x.Employee)
                 .Include(x => x.EmployeeCreated)
                 .Include(x => x.EmployeeUpdated)
                 .Include(x => x.OrderDetails)
                 .ThenInclude(x => x.DesignSample)
                 .ThenInclude(x => x.MasterDesignCategory)
                 .Where(x => x.IsDeleted &&
                  (pagingRequest.SalesmanId == 0 || x.EmployeeId == pagingRequest.SalesmanId) &&
                             x.DeletedDate.Date >= pagingRequest.FromDate.Date &&
                            x.DeletedDate.Date <= pagingRequest.ToDate.Date)
                 .OrderByDescending(x => x.OrderNo)
                 .ToListAsync();


            PagingResponse<Order> pagingResponse = new PagingResponse<Order>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<PagingResponse<Order>> GetOrdersByDeliveryDate(DateTime fromDate, DateTime toDate, PagingRequest pagingRequest)
        {
            var data = await _context
                 .Orders
                .Include(x => x.Customer)
                 .Include(x => x.Employee)
                 .Include(x => x.CancelledOrder)
                 .Include(x => x.EmployeeCreated)
                 .Include(x => x.EmployeeUpdated)
                 .Include(x => x.OrderDetails)
                 .ThenInclude(x => x.DesignSample)
                 .ThenInclude(x => x.MasterDesignCategory)
                 .Where(x => x.OrderDeliveryDate.Date >= fromDate.Date && 
                             x.OrderDeliveryDate.Date <= toDate.Date && 
                             !x.IsCancelled && 
                             !x.IsDeleted &&
                             x.Status==OrderStatusEnum.Completed.ToString())
                 .OrderByDescending(x => x.OrderDeliveryDate)
                 .ToListAsync();

            PagingResponse<Order> pagingResponse = new PagingResponse<Order>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<PagingResponse<Order>> SearchOrdersByDeliveryDate(SearchPagingRequest searchPagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context
                 .Orders
                .Include(x => x.Customer)
                 .Include(x => x.Employee)
                 .Include(x => x.CancelledOrder)
                 .Include(x => x.EmployeeCreated)
                 .Include(x => x.EmployeeUpdated)
                 .Include(x => x.OrderDetails)
                 .ThenInclude(x => x.DesignSample)
                 .ThenInclude(x => x.MasterDesignCategory)
               .Where(x => !x.IsDeleted && 
               x.Status==OrderStatusEnum.Completed.ToString() &&
               !x.IsCancelled && (
                        searchTerm.Equals(string.Empty) ||
                        (x.Employee.FirstName + " " + x.Employee.LastName).Contains(searchTerm) ||
                        x.TotalAmount.ToString().Contains(searchTerm) ||
                        x.BalanceAmount.ToString().Contains(searchTerm) ||
                        x.PaymentMode.Equals(searchTerm) ||
                        (x.Customer.Firstname + " " + x.Customer.Lastname).Contains(searchTerm) ||
                        x.City.Contains(searchTerm) ||
                        x.OrderDetails.Any(x => x.OrderNo.Contains(searchTerm)) ||
                        x.Customer.Contact1.Contains(searchTerm) ||
                        x.Customer.Contact2.Contains(searchTerm) ||
                        x.CustomerRefName.Contains(searchTerm) ||
                        x.OrderNo.Contains(searchTerm)))
                 .OrderByDescending(x => x.OrderDeliveryDate)
                 .ToListAsync();

            PagingResponse<Order> pagingResponse = new PagingResponse<Order>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<PagingResponse<Order>> SearchCancelledOrders(SearchPagingRequest searchPagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context
                .Orders
               .Include(x => x.Customer)
                .Include(x => x.Employee)
                .Include(x => x.EmployeeCreated)
                .Include(x => x.EmployeeUpdated)
                .Include(x => x.OrderDetails.Where(y => y.IsCancelled))
                .ThenInclude(x => x.DesignSample)
                .ThenInclude(x => x.MasterDesignCategory)
                .Where(x => (x.OrderDetails.Where(y => y.IsCancelled &&
                            y.CancelledDate.Date >= searchPagingRequest.FromDate.Date &&
                            y.CancelledDate.Date <= searchPagingRequest.ToDate.Date).Count() > 0) && (
                        searchTerm.Equals(string.Empty) ||
                        x.TotalAmount.ToString().Contains(searchTerm) ||
                        x.BalanceAmount.ToString().Contains(searchTerm) ||
                         x.PaymentMode.Equals(searchTerm) ||
                              (x.Customer.Firstname + " " + x.Customer.Lastname).Contains(searchTerm) ||
                            (x.EmployeeCreated.FirstName + " " + x.EmployeeCreated.LastName).Contains(searchTerm) ||
                              (x.Employee.FirstName + " " + x.Employee.LastName).Contains(searchTerm) ||
                        x.City.Contains(searchTerm) ||
                        x.OrderDetails.Any(x => x.OrderNo.Contains(searchTerm)) ||
                        x.Customer.Contact1.Contains(searchTerm) ||
                         x.Customer.Contact2.Contains(searchTerm) ||
                        x.CustomerRefName.Contains(searchTerm) ||
                        x.OrderNo.Contains(searchTerm)))
                .OrderByDescending(x => x.OrderNo)
                .ToListAsync();

            PagingResponse<Order> pagingResponse = new PagingResponse<Order>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<PagingResponse<Order>> SearchDeletedOrders(SearchPagingRequest searchPagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context
                .Orders
               .Include(x => x.Customer)
                .Include(x => x.Employee)
                .Include(x => x.EmployeeCreated)
                .Include(x => x.EmployeeUpdated)
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.DesignSample)
                .ThenInclude(x => x.MasterDesignCategory)
                .Where(x => x.IsDeleted && x.DeletedDate.Date >= searchPagingRequest.FromDate.Date &&
                        x.DeletedDate.Date <= searchPagingRequest.ToDate.Date && (
                        searchTerm.Equals(string.Empty) ||
                        x.TotalAmount.ToString().Contains(searchTerm) ||
                        x.BalanceAmount.ToString().Contains(searchTerm) ||
                        x.PaymentMode.Equals(searchTerm) ||
                        (x.Customer.Firstname + " " + x.Customer.Lastname).Contains(searchTerm) ||
                        (x.EmployeeCreated.FirstName + " " + x.EmployeeCreated.LastName).Contains(searchTerm) ||
                        (x.Employee.FirstName + " " + x.Employee.LastName).Contains(searchTerm) ||
                        x.City.Contains(searchTerm) ||
                        x.OrderDetails.Any(x => x.OrderNo.Contains(searchTerm)) ||
                        x.Customer.Contact1.Contains(searchTerm) ||
                        x.Customer.Contact2.Contains(searchTerm) ||
                        x.CustomerRefName.Contains(searchTerm) ||
                        x.OrderNo.Contains(searchTerm)))
                .OrderByDescending(x => x.OrderNo)
                .ToListAsync();
            PagingResponse<Order> pagingResponse = new PagingResponse<Order>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<int> Delete(int orderId, string note)
        {
            Order oldOrder = await _context.Orders
             .Include(x => x.AccountStatements)
             .Include(x => x.OrderDetails)
             .Where(order => order.Id == orderId)
             .FirstOrDefaultAsync();

            if (oldOrder == null)
                throw new BusinessRuleViolationException(StaticValues.OrderNotFoundError, StaticValues.OrderNotFoundMessage);

            if (oldOrder.IsDeleted)
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyDeletedError, StaticValues.RecordAlreadyDeletedMessage);

            if (oldOrder.IsCancelled)
                throw new BusinessRuleViolationException(StaticValues.OrderAlreadyCancelledError, StaticValues.OrderAlreadyCancelledMessage);
            if (oldOrder.Status != OrderStatusEnum.Active.ToString())
            {
                throw new BusinessRuleViolationException(StaticValues.ActiveOrderDeleteError, StaticValues.ActiveOrderDeleteMessage);
            }

            if (oldOrder.OrderDetails.Count(x => x.IsCancelled) > 0)
                throw new BusinessRuleViolationException(StaticValues.OrderPartiallyCancelledError, StaticValues.OrderPartiallyCancelledMessage);

            var trans = _context.Database.BeginTransaction();
            oldOrder.IsDeleted = true;
            oldOrder.DeletedDate = DateTime.Now;
            oldOrder.Note = note;
            oldOrder.Status = OrderStatusEnum.Deleted.ToString();
            EntityEntry<Order> entity = _context.Orders.Update(oldOrder);
            entity.State = EntityState.Modified;
            if (await _context.SaveChangesAsync() > 0)
            {
                var orderDetailIds = oldOrder.OrderDetails.Select(x => x.Id).ToList();
                string allOrderNo = string.Empty;
                foreach (OrderDetail orderDetail in oldOrder.OrderDetails)
                {
                    orderDetail.IsDeleted = true;
                    allOrderNo = $"{orderDetail.OrderNo},";
                    orderDetail.Status = OrderStatusEnum.Deleted.ToString();
                }
                var accountStatement = new CustomerAccountStatement()
                {
                    CustomerId = oldOrder.CustomerId,
                    OrderId = orderId,
                    Credit = oldOrder.BalanceAmount,
                    Remark = $"Order No-{allOrderNo}{oldOrder.OrderNo}",
                    PaymentDate = DateTime.Now.Date,
                    PaymentMode = PaymentModeEnum.Cash.ToString(),
                    Reason = AccountStatementReasonEnum.OrderDeleted.ToString()
                };
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    var workResult = await _workTypeStatusRepository.DeleteByOrderDetailId(orderDetailIds);
                    if (workResult > 0)
                    {
                        trans.Commit();
                        return workResult;
                    }
                }
            }
            trans.Rollback();
            return default;
        }

        public async Task<PagingResponse<Order>> GetOrdersByCustomer(PagingRequest pagingRequest, int customerId, string orderStatus)
        {

            var data = await _context
                .Orders
               .Include(x => x.Customer)
                .Include(x => x.Employee)
                .Include(x => x.CancelledOrder)
                .Include(x => x.EmployeeCreated)
                .Include(x => x.EmployeeUpdated)
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.DesignSample)
                .ThenInclude(x => x.MasterDesignCategory)
                .Where(x => x.CustomerId == customerId &&
                !x.IsDeleted && x.OrderDate.Date >= pagingRequest.FromDate.Date &&
                  (orderStatus == "0" || x.Status.ToLower() == orderStatus) &&
                x.OrderDate.Date <= pagingRequest.ToDate.Date)
                .OrderByDescending(x => x.OrderNo)
                .ToListAsync();

            PagingResponse<Order> pagingResponse = new PagingResponse<Order>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<PagingResponse<Order>> GetOrdersBySalesman(PagingRequest pagingRequest, int salesmanId, string orderStatus)
        {

            var data = await _context
                .Orders
               .Include(x => x.Customer)
                .Include(x => x.Employee)
                .Include(x => x.CancelledOrder)
                .Include(x => x.EmployeeCreated)
                .Include(x => x.EmployeeUpdated)
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.DesignSample)
                .ThenInclude(x => x.MasterDesignCategory)
                .Where(x => x.EmployeeId == salesmanId &&
                !x.IsDeleted && x.OrderDate.Date >= pagingRequest.FromDate.Date &&
                 (orderStatus == "0" || x.Status.ToLower() == orderStatus) &&
                x.OrderDate.Date <= pagingRequest.ToDate.Date)
                .OrderByDescending(x => x.OrderNo)
                .ToListAsync();

            PagingResponse<Order> pagingResponse = new PagingResponse<Order>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<List<OrderIdNumberResponse>> GetAllOrderNos()
        {
            return await _context.Orders
                .Where(x => !x.IsCancelled && !x.IsDeleted)
                .OrderByDescending(x => x.Id)
                .Select(x => new OrderIdNumberResponse() { OrderNo = x.OrderNo, OrderId = x.Id })
                .ToListAsync();
        }

        public async Task<List<OrderDetail>> GetOrderDetails(string orderNo)
        {
            var order = await _context.Orders.Where(x => x.OrderNo == orderNo).FirstOrDefaultAsync();
            if (order == null)
                return default;
            return await _context.OrderDetails
                 .Include(x => x.EmployeeCreated)
                 .Include(x => x.EmployeeUpdated)
                 .Include(x => x.DesignSample)
                 .ThenInclude(x => x.MasterDesignCategory)
                 .Where(x => !x.IsCancelled && !x.IsDeleted && x.OrderId == order.Id)
                 .ToListAsync();
        }

        public async Task<PagingResponse<Order>> GetOrdersBySalesmanAndDateRange(PagingRequest pagingRequest, int salesmanId, DateTime fromDate, DateTime toDate)
        {
            var data = await _context
                .Orders
               .Include(x => x.Customer)
                .Include(x => x.Employee)
                .Include(x => x.CancelledOrder)
                .Include(x => x.EmployeeCreated)
                .Include(x => x.EmployeeUpdated)
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.DesignSample)
                .ThenInclude(x => x.MasterDesignCategory)
                .Where(x => !x.IsCancelled && !x.IsDeleted && x.EmployeeId == salesmanId && x.CreatedAt >= fromDate && x.CreatedAt <= toDate)
                .OrderByDescending(x => x.OrderNo)
                .ToListAsync();

            PagingResponse<Order> pagingResponse = new PagingResponse<Order>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<PagingResponse<Order>> SearchByCustomer(SearchPagingRequest searchPagingRequest, int customerId)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.Orders
                .Include(x => x.Customer)
                .Include(x => x.Employee)
                .Include(x => x.EmployeeCreated)
                .Include(x => x.EmployeeUpdated)
                .Include(x => x.CancelledOrder)
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.DesignSample)
                .ThenInclude(x => x.MasterDesignCategory)
                .Where(order => !order.IsDeleted && (
                        searchTerm.Equals(string.Empty) ||
                        order.TotalAmount.ToString().Contains(searchTerm) ||
                        order.BalanceAmount.ToString().Contains(searchTerm) ||
                         order.PaymentMode.Equals(searchTerm) ||
                              (order.Customer.Firstname + " " + order.Customer.Lastname).Contains(searchTerm) ||
                            (order.EmployeeCreated.FirstName + " " + order.EmployeeCreated.LastName).Contains(searchTerm) ||
                              (order.Employee.FirstName + " " + order.Employee.LastName).Contains(searchTerm) ||
                        order.City.Contains(searchTerm) ||
                        order.OrderDetails.Any(x => x.OrderNo.Contains(searchTerm)) ||
                        order.Customer.Contact1.Contains(searchTerm) ||
                        order.CustomerRefName.Contains(searchTerm) ||
                        order.OrderNo.Contains(searchTerm))
                    )
                .OrderByDescending(x => x.OrderNo)
                .ToListAsync();
            PagingResponse<Order> pagingResponse = new PagingResponse<Order>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<PagingResponse<Order>> SearchBySalesman(SearchPagingRequest searchPagingRequest, int salesmanId)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.Orders
                .Include(x => x.Customer)
                .Include(x => x.Employee)
                .Include(x => x.EmployeeCreated)
                .Include(x => x.EmployeeUpdated)
                .Include(x => x.CancelledOrder)
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.DesignSample)
                .ThenInclude(x => x.MasterDesignCategory)
                .Where(order => !order.IsDeleted && (
                        searchTerm.Equals(string.Empty) ||
                        order.TotalAmount.ToString().Contains(searchTerm) ||
                        order.BalanceAmount.ToString().Contains(searchTerm) ||
                        order.PaymentMode.Equals(searchTerm) ||
                              (order.Customer.Firstname + " " + order.Customer.Lastname).Contains(searchTerm) ||
                            (order.EmployeeCreated.FirstName + " " + order.EmployeeCreated.LastName).Contains(searchTerm) ||
                              (order.Employee.FirstName + " " + order.Employee.LastName).Contains(searchTerm) ||
                        order.City.Contains(searchTerm) ||
                        order.OrderDetails.Any(x => x.OrderNo.Contains(searchTerm)) ||
                        order.Customer.Contact1.Contains(searchTerm) ||
                        order.CustomerRefName.Contains(searchTerm) ||
                        order.OrderNo.Contains(searchTerm))
                    )
                .OrderByDescending(x => x.OrderNo)
                .ToListAsync();
            PagingResponse<Order> pagingResponse = new PagingResponse<Order>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<PagingResponse<Order>> SearchBySalesmanAndDateRange(SearchPagingRequest searchPagingRequest, int salesmanId, DateTime fromDate, DateTime toDate)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.Orders
                .Include(x => x.Customer)
                .Include(x => x.Employee)
                .Include(x => x.EmployeeCreated)
                .Include(x => x.EmployeeUpdated)
                .Include(x => x.CancelledOrder)
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.DesignSample)
                .ThenInclude(x => x.MasterDesignCategory)
                .Where(order => order.EmployeeId == salesmanId &&
                order.CreatedAt >= fromDate &&
                order.CreatedAt <= toDate &&
                !order.IsDeleted &&
                !order.IsCancelled &&
                (
                        searchTerm.Equals(string.Empty) ||
                        order.TotalAmount.ToString().Contains(searchTerm) ||
                        order.BalanceAmount.ToString().Contains(searchTerm) ||
                         order.PaymentMode.Equals(searchTerm) ||
                              (order.Customer.Firstname + " " + order.Customer.Lastname).Contains(searchTerm) ||
                            (order.EmployeeCreated.FirstName + " " + order.EmployeeCreated.LastName).Contains(searchTerm) ||
                        order.City.Contains(searchTerm) ||
                        order.OrderDetails.Any(x => x.OrderNo.Contains(searchTerm)) ||
                        order.Customer.Contact1.Contains(searchTerm) ||
                        order.CustomerRefName.Contains(searchTerm) ||
                        order.OrderNo.Contains(searchTerm))
                    )
                .OrderByDescending(x => x.OrderNo)
                .ToListAsync();
            PagingResponse<Order> pagingResponse = new PagingResponse<Order>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<bool> IsOrderNoExist(string OrderNo)
        {
            return await _context.Orders.Where(x => x.OrderNo == OrderNo).CountAsync() > 0;
        }

        public async Task<PagingResponse<OrderDetail>> GetOrderAlerts(OrderAlertRequest orderAlertRequest)
        {
            var data = await _context
                .OrderDetails
                .Include(x => x.WorkTypeStatuses)
                .ThenInclude(x => x.WorkType)
               .Include(x => x.Order)
               .ThenInclude(x => x.Customer)
               .Include(x => x.Order)
               .ThenInclude(x => x.Employee)
                .Where(x =>
                !x.IsCancelled &&
                !x.IsDeleted &&
                (orderAlertRequest.SalesmanId == 0 || x.Order.EmployeeId == orderAlertRequest.SalesmanId) &&
                x.DeliveredDate != null &&
                (x.Status == OrderStatusEnum.Active.ToString() || x.Status == OrderStatusEnum.Processing.ToString() || x.Status == OrderStatusEnum.PartiallyDelivered.ToString() || x.Status == OrderStatusEnum.Completed.ToString()) &&
                x.OrderDeliveryDate.Date >= orderAlertRequest.FromDate.Date &&
                 x.OrderDeliveryDate.Date <= orderAlertRequest.ToDate.AddDays(orderAlertRequest.AlertBeforeDays).Date
                )
                .OrderByDescending(x => x.Order.OrderNo)
                .ToListAsync();

            var filterData = data.Where(x => x.WorkTypeStatuses.Where(y => y.CompletedBy == null).Count() > 0).ToList();
            PagingResponse<OrderDetail> pagingResponse = new PagingResponse<OrderDetail>()
            {
                PageNo = orderAlertRequest.PageNo,
                PageSize = orderAlertRequest.PageSize,
                Data = filterData.Skip(orderAlertRequest.PageSize * (orderAlertRequest.PageNo - 1)).Take(orderAlertRequest.PageSize).ToList(),
                TotalRecords = filterData.Count
            };
            return pagingResponse;
        }
        public async Task<PagingResponse<OrderDetail>> SearchOrderAlert(SearchPagingRequest searchPagingRequest, int daysBefore)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context
               .OrderDetails
               .Include(x => x.WorkTypeStatuses)
                .ThenInclude(x => x.WorkType)
               .Include(x => x.DesignSample)
               .ThenInclude(x => x.MasterDesignCategory)
              .Include(x => x.Order)
              .ThenInclude(x => x.Customer)
              .Include(x => x.Order)
               .ThenInclude(x => x.Employee)
               .Where(x =>
               !x.IsCancelled &&
               !x.IsDeleted &&
                   (x.Status == OrderStatusEnum.Active.ToString() || x.Status == OrderStatusEnum.Processing.ToString()) &&
                   (
                       searchTerm == "" ||
                       x.Order.OrderNo.Contains(searchTerm) ||
                       x.Order.Customer.Contact1.Contains(searchTerm) ||
                       x.Order.Customer.Contact2.Contains(searchTerm) ||
                        x.Order.PaymentMode.Equals(searchTerm) ||
                          (x.Order.Employee.FirstName + " " + x.Order.Employee.LastName).Contains(searchTerm) ||
                              (x.Order.Customer.Firstname + " " + x.Order.Customer.Lastname).Contains(searchTerm) ||
                            (x.Order.EmployeeCreated.FirstName + " " + x.Order.EmployeeCreated.LastName).Contains(searchTerm) ||
                       x.OrderNo.Contains(searchTerm) ||
                       x.TotalAmount.ToString().Contains(searchTerm) ||
                       x.WorkType.Contains(searchTerm) ||
                       x.Description.Contains(searchTerm)
                   )
               )
               .OrderByDescending(x => x.OrderNo)
               .ToListAsync();
            var filterData = data.Where(x => x.WorkTypeStatuses.Where(y => y.CompletedBy == null).Count() > 0).ToList();
            PagingResponse<OrderDetail> pagingResponse = new PagingResponse<OrderDetail>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = filterData.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = filterData.Count
            };
            return pagingResponse;
        }
        public async Task<int> DeliveryPayment(DeliveryPaymentRequest deliveryPaymentRequest)
        {
            if (deliveryPaymentRequest.AllDelivery)
            {
                if (!await _workTypeStatusRepository.IsAllKandooraCompleted(deliveryPaymentRequest.OrderId))
                {
                    throw new BusinessRuleViolationException(StaticValues.OrderNotCompletedError, StaticValues.OrderNotCompletedMessage);
                }
            }
            else
            {
                if (!await _workTypeStatusRepository.IsKandooraCompleted(deliveryPaymentRequest.DeliveredKandoorIds))
                {
                    throw new BusinessRuleViolationException(StaticValues.KandooraNotCompletedError, "Some " + StaticValues.KandooraNotCompletedMessage);
                }
            }
            var accountStatement = new CustomerAccountStatement()
            {
                CustomerId = deliveryPaymentRequest.CustomerId,
                OrderId = deliveryPaymentRequest.OrderId,
                Credit = deliveryPaymentRequest.PaidAmount,
                DeliveredQty = deliveryPaymentRequest.DeliveredKandoorIds.Count,
                Balance = deliveryPaymentRequest.OrderBalanceAmount,
                PaymentMode = deliveryPaymentRequest.PaymentMode,
                Remark = $"{(deliveryPaymentRequest.AllDelivery ? deliveryPaymentRequest.TotalKandoorInOrder : deliveryPaymentRequest.DeliveredKandoorIds.Count)} Delivered ## Order No- {deliveryPaymentRequest.OrderNo} ## OrderDetailId {string.Join(",", deliveryPaymentRequest.DeliveredKandoorIds)}",
                PaymentDate = deliveryPaymentRequest.DeliveredOn.Date,
                Reason = AccountStatementReasonEnum.PaymentReceived.ToString()
            };
            var trans = await _context.Database.BeginTransactionAsync();
            _context.CustomerAccountStatements.Add(accountStatement);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                if (deliveryPaymentRequest.AllDelivery)
                {
                    await ChangeOrderStatus(deliveryPaymentRequest.OrderId, OrderStatusEnum.Delivered, "Change Status - " + OrderStatusEnum.Delivered.ToString(), deliveryPaymentRequest.DeliveredOn);
                }
                else
                {

                    var orderStatusResult = await ChangeOrderDetailStatus(deliveryPaymentRequest.DeliveredKandoorIds, OrderStatusEnum.Delivered, "Change Status - " + OrderStatusEnum.Delivered.ToString(), deliveryPaymentRequest.DeliveredOn);
                    if (orderStatusResult > 0)
                    {
                        if (await ChangeStatusIfAllKandooraDeliverd(deliveryPaymentRequest.OrderId) == 0)
                            await ChangeOrderStatus(deliveryPaymentRequest.OrderId, OrderStatusEnum.PartiallyDelivered, "Change Order Status", deliveryPaymentRequest.DeliveredOn);
                    }
                    else
                    {
                        await trans.RollbackAsync();
                    }

                }
                await UpdateOrderBalanceAmount(deliveryPaymentRequest.OrderId, deliveryPaymentRequest.PaidAmount);
                await trans.CommitAsync();
            }
            else
            {
                await trans.RollbackAsync();
            }
            return result;
        }
        public async Task<LastPaymentResponse> GetPaidAmountByCustomer(int orderId)
        {
            var statements = await _context.CustomerAccountStatements
                 .Where(x => x.OrderId == orderId && x.Reason.Contains(AccountStatementReasonEnum.PaymentReceived.ToString()))
                 .OrderByDescending(x => x.PaymentDate)
                 .ToListAsync();

            if (statements.Count == 0)
                return new LastPaymentResponse();
            return new LastPaymentResponse()
            {
                LastPaidAmount = statements.FirstOrDefault().Credit,
                TotalPaidAmount = statements.Sum(x => x.Credit)
            };
        }
        public async Task<int> ChangeOrderStatus(int orderId, OrderStatusEnum orderStatus, string note, DateTime? deliveredOn)
        {
            Order oldOrder = await _context.Orders.Include(x => x.OrderDetails)
             .Where(order => order.Id == orderId)
             .FirstOrDefaultAsync();

            if (oldOrder == null)
                throw new BusinessRuleViolationException(StaticValues.OrderNotFoundError, StaticValues.OrderNotFoundMessage);

            if (oldOrder.IsDeleted)
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyDeletedError, StaticValues.RecordAlreadyDeletedMessage);

            if (oldOrder.IsCancelled)
                throw new BusinessRuleViolationException(StaticValues.OrderAlreadyCancelledError, StaticValues.OrderAlreadyCancelledMessage);

            if (oldOrder.Status == OrderStatusEnum.Delivered.ToString())
                throw new BusinessRuleViolationException(StaticValues.OrderAlreadyDeliveredError, StaticValues.OrderAlreadyDeliverdMessage);
            if (oldOrder.Status == OrderStatusEnum.PartiallyDelivered.ToString() && orderStatus != OrderStatusEnum.Delivered)
                return 0;
            if (orderStatus == OrderStatusEnum.Delivered)
            {
                oldOrder.TaxInvoiceNo = await GetNextTaxInvoiceNo((DateTime)deliveredOn);
            }

            oldOrder.Status = orderStatus.ToString();
            oldOrder.Note = note;
            EntityEntry<Order> entity = _context.Orders.Update(oldOrder);
            entity.State = EntityState.Modified;
            var result = await _context.SaveChangesAsync();
            if (result > 0 && orderStatus == OrderStatusEnum.Delivered)
            {
                var orderDetailIds = oldOrder.OrderDetails.Select(x => x.Id).ToList();
                await ChangeOrderDetailStatus(orderDetailIds, orderStatus, note, deliveredOn);
            }

            return result;
        }
        public async Task<int> ChangeOrderDetailStatus(int orderDetailId, OrderStatusEnum orderStatus, string note, DateTime? deliveredOn = null)
        {
            OrderDetail oldOrderDetail = await _context.OrderDetails.Include(x => x.Order)
            .Where(order => order.Id == orderDetailId)
            .FirstOrDefaultAsync();

            if (oldOrderDetail == null)
                throw new BusinessRuleViolationException(StaticValues.OrderNotFoundError, StaticValues.OrderNotFoundMessage);

            if (oldOrderDetail.IsDeleted)
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyDeletedError, StaticValues.RecordAlreadyDeletedMessage);

            if (oldOrderDetail.IsCancelled)
                throw new BusinessRuleViolationException(StaticValues.OrderAlreadyCancelledError, StaticValues.OrderAlreadyCancelledMessage);

            if (oldOrderDetail.Status == OrderStatusEnum.Delivered.ToString())
                throw new BusinessRuleViolationException(StaticValues.OrderAlreadyDeliveredError, StaticValues.OrderAlreadyDeliverdMessage);

            oldOrderDetail.Status = orderStatus.ToString();
            oldOrderDetail.Note = note;
            if (orderStatus == OrderStatusEnum.Delivered)
            {
                if (deliveredOn == null)
                    oldOrderDetail.DeliveredDate = DateTime.UtcNow.AddHours(4);
                else
                    oldOrderDetail.DeliveredDate = (DateTime)deliveredOn;
            }

            EntityEntry<OrderDetail> entity = _context.OrderDetails.Update(oldOrderDetail);
            entity.State = EntityState.Modified;
            var result = await _context.SaveChangesAsync();

            if (orderStatus == OrderStatusEnum.Delivered)
            {
                var totalKandooraNotDelivered = await _context.OrderDetails.Where(x => x.OrderId == oldOrderDetail.OrderId && x.Status != OrderStatusEnum.Delivered.ToString()).CountAsync();
                string newStatus = OrderStatusEnum.Delivered.ToString();
                OrderStatusEnum orderStatusEnum = OrderStatusEnum.Delivered;
                if (totalKandooraNotDelivered > 0)
                {
                    newStatus = OrderStatusEnum.PartiallyDelivered.ToString();
                    orderStatusEnum = OrderStatusEnum.PartiallyDelivered;
                }
                await ChangeOrderStatus(oldOrderDetail.OrderId, orderStatusEnum, "Change Status - " + newStatus, deliveredOn);
                return result;
            }
            if (orderStatus == OrderStatusEnum.Completed)
            {
                var totalKandooraNotDelivered = await _context.OrderDetails.Where(x => x.OrderId == oldOrderDetail.OrderId && x.Status != OrderStatusEnum.Completed.ToString()).CountAsync();
                if (totalKandooraNotDelivered == 0)
                {
                    await ChangeOrderStatus(oldOrderDetail.OrderId, OrderStatusEnum.Completed, "Change Status - " + OrderStatusEnum.Completed.ToString(), deliveredOn);
                    return result;
                }
                else
                {
                    await ChangeOrderStatus(oldOrderDetail.OrderId, OrderStatusEnum.Processing, "Change Status - " + OrderStatusEnum.Processing.ToString(), deliveredOn);
                    return result;
                }
            }
            await ChangeOrderStatus(oldOrderDetail.OrderId, orderStatus, "Change Status - " + orderStatus.ToString(), deliveredOn);
            return result;
        }
        public async Task<int> ChangeOrderDetailStatus(List<int> orderDetailIds, OrderStatusEnum orderStatus, string note, DateTime? DeliveredOn = null)
        {
            List<OrderDetail> oldOrderDetails = await _context.OrderDetails
            .Where(order => orderDetailIds.Contains(order.Id))
            .ToListAsync();

            if (oldOrderDetails.Count == 0)
                throw new BusinessRuleViolationException(StaticValues.OrderNotFoundError, "Some " + StaticValues.OrderNotFoundMessage);

            if (oldOrderDetails.Where(x => x.IsDeleted).Count() > 0)
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyDeletedError, "Some " + StaticValues.RecordAlreadyDeletedMessage);

            if (oldOrderDetails.Where(x => x.IsCancelled).Count() > 0)
                throw new BusinessRuleViolationException(StaticValues.OrderAlreadyCancelledError, "Some " + StaticValues.OrderAlreadyCancelledMessage);

            if (oldOrderDetails.Where(x => x.Status == OrderStatusEnum.Delivered.ToString()).Count() > 0)
                throw new BusinessRuleViolationException(StaticValues.OrderAlreadyDeliveredError, "Some " + StaticValues.OrderAlreadyDeliverdMessage);

            if (orderStatus == OrderStatusEnum.Delivered && oldOrderDetails.Where(x => x.Status != OrderStatusEnum.Completed.ToString()).Count() > 0)
                throw new BusinessRuleViolationException(StaticValues.OrderNotCompletedError, "Some " + StaticValues.OrderNotCompletedMessage);

            oldOrderDetails.ForEach(x =>
            {
                x.Status = orderStatus.ToString();

                x.Note = note;
                if (orderStatus == OrderStatusEnum.Delivered)
                {
                    if (DeliveredOn == null)
                        x.DeliveredDate = DateTime.UtcNow.AddHours(4);
                    else
                        x.DeliveredDate = (DateTime)DeliveredOn;
                }
            });

            _context.AttachRange(oldOrderDetails);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> GetDesignSamplesCountInPreOrder(int customerId, int sampleId)
        {
            return await _context.OrderDetails
                .Include(x => x.Order)
                .Where(x => x.DesignSampleId == sampleId && x.Order.CustomerId == customerId)
                .CountAsync();
        }
        public async Task<OrderQuantitiesResponse> GetOrderQuantities(DateTime fromDate, DateTime toDate)
        {
            try
            {
                var orderDetails = await _context.OrderDetails.Where(x => x.CreatedAt != null && x.CreatedAt >= fromDate && x.CreatedAt <= toDate).ToListAsync();
                var orders = await _context.Orders.Where(x => !x.IsCancelled && !x.IsDeleted && x.OrderDate >= fromDate && x.OrderDate <= toDate).ToListAsync();
                OrderQuantitiesResponse orderQuantitiesResponse = new OrderQuantitiesResponse
                {
                    OrderQty = orders.Count(),
                    ActiveQty = orderDetails.Where(x => !x.IsDeleted &&
                                                                        !x.IsCancelled &&
                                                                              x.Status == null || x.Status == OrderStatusEnum.Active.ToString()).Count(),
                    BookingQty = orderDetails.Where(x => !x.IsDeleted && !x.IsCancelled).Count(),
                    CancelledQty = orderDetails.Where(x => x.IsCancelled && !x.IsDeleted).Count(),
                    DeletedQty = orderDetails.Where(x => !x.IsCancelled && x.IsDeleted).Count(),
                    CompletedQty = orderDetails.Where(x => !x.IsDeleted &&
                                                                          !x.IsCancelled &&
                                                                                  x.Status == OrderStatusEnum.Completed.ToString()).Count(),
                    DeliveredQty = orderDetails.Where(x => !x.IsDeleted &&
                                                                           !x.IsCancelled &&
                                                                                   x.Status == OrderStatusEnum.Delivered.ToString()).Count(),
                    ProcessingQty = orderDetails.Where(x => !x.IsDeleted &&
                                                                           !x.IsCancelled &&
                                                                                   x.Status == OrderStatusEnum.Processing.ToString()).Count(),
                    AlertQty = orderDetails.Where(x => x.OrderDeliveryDate.Date >= DateTime.Now.Date && x.OrderDeliveryDate <= DateTime.Now.AddDays(15)).Count()
                };
                return orderQuantitiesResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<PagingResponse<Order>> GetPendingOrders(PagingRequestByWorkType pagingRequest)
        {
            var data = await _context
                 .Orders
                .Include(x => x.Customer)
                 .Include(x => x.Employee)
                 .Include(x => x.CancelledOrder)
                 .Include(x => x.EmployeeCreated)
                 .Include(x => x.EmployeeUpdated)
                 .Include(x => x.OrderDetails.Where(y => !y.IsCancelled && !y.IsDeleted && y.Status != OrderStatusEnum.Delivered.ToString()))
                 .ThenInclude(x => x.DesignSample)
                 .ThenInclude(x => x.MasterDesignCategory)
                 .Where(x => !x.IsCancelled &&
                 !x.IsDeleted &&
                  (pagingRequest.SalesmanId == 0 || x.EmployeeId == pagingRequest.SalesmanId) &&
                 x.OrderDate.Date >= pagingRequest.FromDate.Date &&
                 (pagingRequest.SalesmanId==0 || x.EmployeeId==pagingRequest.SalesmanId) &&
                 x.OrderDate.Date <= pagingRequest.ToDate.Date &&
                 x.Status != OrderStatusEnum.Delivered.ToString())
                 .OrderByDescending(x => x.OrderNo)
                 .ToListAsync();

            PagingResponse<Order> pagingResponse = new PagingResponse<Order>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }
        public async Task<PagingResponse<Order>> SearchPendingOrders(SearchPagingRequest searchPagingRequest, DateTime fromDate, DateTime toDate)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.Orders
                .Include(x => x.Customer)
                .Include(x => x.Employee)
                .Include(x => x.EmployeeCreated)
                .Include(x => x.EmployeeUpdated)
                .Include(x => x.CancelledOrder)
                .Include(x => x.OrderDetails.Where(y => !y.IsCancelled && !y.IsDeleted && y.Status != OrderStatusEnum.Delivered.ToString()))
                .ThenInclude(x => x.DesignSample)
                .ThenInclude(x => x.MasterDesignCategory)
                .Where(order => !order.IsCancelled &&
                !order.IsDeleted &&
                order.Status != OrderStatusEnum.Delivered.ToString() && (
                        searchTerm.Equals(string.Empty) ||
                        order.PaymentMode.Equals(searchTerm) ||
                        order.TotalAmount.ToString().Contains(searchTerm) ||
                        order.BalanceAmount.ToString().Contains(searchTerm) ||
                          (order.Customer.Firstname + " " + order.Customer.Lastname).Contains(searchTerm) ||
                            (order.EmployeeCreated.FirstName + " " + order.EmployeeCreated.LastName).Contains(searchTerm) ||
                              (order.Employee.FirstName + " " + order.Employee.LastName).Contains(searchTerm) ||
                        order.City.Contains(searchTerm) ||
                        order.OrderDetails.Any(x => x.OrderNo.Contains(searchTerm)) ||
                        order.Customer.Contact1.Contains(searchTerm) ||
                         order.Customer.Contact2.Contains(searchTerm) ||
                        order.CustomerRefName.Contains(searchTerm) ||
                        order.OrderNo.Contains(searchTerm))
                    )
                .OrderByDescending(x => x.OrderNo)
                .ToListAsync();
            PagingResponse<Order> pagingResponse = new PagingResponse<Order>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }
        public async Task<PagingResponse<Order>> SearchWithFilter(OrderSearchPagingRequest searchPagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.Orders
                .Include(x => x.Customer)
                .Include(x => x.Employee)
                .Include(x => x.EmployeeCreated)
                .Include(x => x.EmployeeUpdated)
                .Include(x => x.CancelledOrder)
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.DesignSample)
                .ThenInclude(x => x.MasterDesignCategory)
                .Where(order => order.OrderDate >= searchPagingRequest.FromDate &&
                         order.OrderDate <= searchPagingRequest.ToDate && !order.IsDeleted &&
                        !order.IsCancelled &&
                        (
                            searchTerm.Equals(string.Empty) ||
                            order.PaymentMode.Equals(searchTerm) ||
                            (order.Customer.Firstname + " " + order.Customer.Lastname).Contains(searchTerm) ||
                            (order.EmployeeCreated.FirstName + " " + order.EmployeeCreated.LastName).Contains(searchTerm) || 
                            (order.Employee.FirstName + " " + order.Employee.LastName).Contains(searchTerm) ||
                            order.City.Contains(searchTerm) ||
                            order.OrderDetails.Any(x => x.OrderNo.Contains(searchTerm)) ||
                            order.Customer.Contact1.Contains(searchTerm) ||
                            order.Customer.Contact2.Contains(searchTerm) ||
                            order.Status.Contains(searchTerm) ||
                            order.TotalAmount.ToString().Equals(searchTerm) ||
                            order.SubTotalAmount.ToString().Equals(searchTerm) ||
                            order.CustomerRefName.Contains(searchTerm) ||
                            order.OrderNo.Contains(searchTerm))
                        )
                .OrderByDescending(x => x.OrderNo)
                .ToListAsync();
            PagingResponse<Order> pagingResponse = new PagingResponse<Order>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;

        }
        public async Task<PagingResponse<OrderDetail>> SearchFilterByWorkType(SearchPagingRequest searchPagingRequest, string workType, string orderStatus = "active")
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context
              .OrderDetails
               .Include(x => x.Order)
               .ThenInclude(x => x.Employee)
               .Include(x => x.Order)
               .ThenInclude(x => x.Customer)
              .Where(x => !x.IsCancelled && !x.IsDeleted && (orderStatus == "0" || x.Status.ToLower() == orderStatus) && x.WorkType.ToLower().Contains(workType) &&
              (searchTerm == "" ||
              x.OrderStatus.ToLower() == searchTerm ||
              x.Status.Contains(searchTerm) ||
              x.OrderNo.Contains(searchTerm) ||
              (x.Order.Customer.Firstname+" "+x.Order.Customer.Lastname).Contains(searchTerm) ||
                (x.Order.Employee.FirstName + " " + x.Order.Employee.LastName).Contains(searchTerm) ||
              x.MeasurementCustomerName.ToLower().Contains(searchTerm) ||
              x.Description.ToLower().Contains(searchTerm) ||
              x.Order.PaymentMode.ToLower().Contains(searchTerm)))
              .OrderByDescending(x => x.OrderId)
              .ToListAsync();

            PagingResponse<OrderDetail> pagingResponse = new PagingResponse<OrderDetail>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }
        public async Task<PagingResponse<OrderDetail>> FilterByWorkType(PagingRequestByWorkType pagingRequest)
        {
            var data = await _context
               .OrderDetails
               .Include(x => x.Order)
               .ThenInclude(x=>x.Employee)
               .Include(x=>x.Order)
               .ThenInclude(x=>x.Customer)
               .Where(x => !x.IsCancelled &&
                           !x.IsDeleted && 
                           x.WorkType.ToLower().Contains(pagingRequest.WorkType) &&
                           x.Order.OrderDeliveryDate.Date >= pagingRequest.FromDate.Date &&
                           x.Order.OrderDeliveryDate.Date <= pagingRequest.ToDate.Date &&
                           (pagingRequest.SalesmanId == 0 || x.Order.EmployeeId == pagingRequest.SalesmanId) &&
                           (pagingRequest.OrderStatus =="0" || x.Status.ToLower() == pagingRequest.OrderStatus.ToLower()))
               .OrderByDescending(x => x.OrderId)
               .ToListAsync();

            PagingResponse<OrderDetail> pagingResponse = new PagingResponse<OrderDetail>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }
        public async Task<int> UpdateModelNo(int orderDetailId, string modelNo)
        {
            var oldOrderDetail = await _context.OrderDetails.Where(x => !x.IsCancelled && !x.IsDeleted && x.Id == orderDetailId).FirstOrDefaultAsync();
            if (oldOrderDetail == null)
                return 0;

            if (oldOrderDetail.Status == OrderStatusEnum.Delivered.ToString())
            {
                throw new BusinessRuleViolationException(StaticValues.OrderAlreadyDeliveredError, StaticValues.OrderAlreadyDeliverdMessage);
            }
            oldOrderDetail.ModelNo = modelNo;
            oldOrderDetail.DesignSampleId = null;
            oldOrderDetail.Note = "Change Model No.";
            var entity = _context.OrderDetails.Attach(oldOrderDetail);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateOrderBalanceAmount(int ordeId, decimal paidAmount)
        {
            var oldOrder = await _context.Orders.Where(x => x.Id == ordeId && !x.IsDeleted && !x.IsCancelled).FirstOrDefaultAsync();
            if (oldOrder == null)
                return 0;
            if (paidAmount > oldOrder.BalanceAmount)
                oldOrder.BalanceAmount = 0;
            else
                oldOrder.BalanceAmount -= paidAmount;
            oldOrder.PaidAmount += paidAmount;
            var entity = _context.Orders.Attach(oldOrder);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();

        }
        public async Task<int> UpdateWorkType(int orderDetailId, string workType)
        {
            if (string.IsNullOrEmpty(workType) || orderDetailId < 1)
            {
                throw new BusinessRuleViolationException(StaticValues.WorkTypeNotEnteredError, StaticValues.WorkTypeNotEnteredMessage);
            }
            var groups = workType.GroupBy(c => c).Where(g => g.Count() > 1).ToList();
            var workTypeList = workType.ToCharArray().ToList();
            if (groups.Count > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.DuplicateWorkTypeError, StaticValues.DuplicateWorkTypeMessage);
            }
            var oldOrderDetail = await _context.OrderDetails.Include(x => x.Order).ThenInclude(x => x.OrderDetails).Where(x => !x.IsCancelled && !x.IsDeleted && x.Id == orderDetailId).FirstOrDefaultAsync();
            if (oldOrderDetail == null)
                return 0;
            if (oldOrderDetail.Order.Status == OrderStatusEnum.Delivered.ToString() || oldOrderDetail.Status == OrderStatusEnum.Delivered.ToString())
            {
                throw new BusinessRuleViolationException(StaticValues.OrderAlreadyDeliveredError, StaticValues.OrderAlreadyDeliverdMessage);
            }


            oldOrderDetail.WorkType = workType;
            if (oldOrderDetail.Order.Status == OrderStatusEnum.Completed.ToString())
            {
                oldOrderDetail.Order.Status = OrderStatusEnum.Processing.ToString();
            }
            var entity = _context.OrderDetails.Attach(oldOrderDetail);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateDesignModel(int orderDetailId, int modelId, string modelNo)
        {
            if (modelId == 0 || orderDetailId == 0)
                return 0;
            var orderDetails = await _context.OrderDetails.Where(x => !x.IsCancelled && !x.IsDeleted && x.Id == orderDetailId).FirstOrDefaultAsync();
            if (orderDetails == null)
                return 0;
            if (orderDetails.Status == OrderStatusEnum.Delivered.ToString())
            {
                throw new BusinessRuleViolationException(StaticValues.OrderAlreadyDeliveredError, StaticValues.OrderAlreadyDeliverdMessage);
            }
            orderDetails.DesignSampleId = modelId;
            orderDetails.Note = "Updated Model";
            orderDetails.ModelNo = modelNo;
            var entity = _context.Attach(orderDetails);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateOrderAdvancePayment(List<CustomerAccountStatement> customerAccountStatements)
        {
            if (customerAccountStatements == null)
                return 0;
            int orderId = 0;
            decimal totalAdvanceAmount = 0;
            customerAccountStatements.ForEach(x =>
            {
                orderId = x.OrderId;
                totalAdvanceAmount += (decimal)x.Credit;
            });

            var oldOrder = await _context.Orders.Where(x => x.Id == orderId).FirstOrDefaultAsync();
            if (oldOrder == null)
                return 0;
            oldOrder.AdvanceAmount += totalAdvanceAmount;
            oldOrder.Note = "Advance Payment";
            oldOrder.BalanceAmount = oldOrder.TotalAmount - oldOrder.AdvanceAmount;
            var entity = _context.Attach(oldOrder);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();

        }
        public async Task<int> UpdateOrderDate(int orderId, DateTime orderDate)
        {
            var oldOrder = await _context.Orders.Where(x => !x.IsCancelled && !x.IsDeleted && x.Id == orderId).FirstOrDefaultAsync();

            if (oldOrder == null)
                throw new BusinessRuleViolationException(StaticValues.OrderNotFoundError, StaticValues.OrderNotFoundMessage);

            if (oldOrder.Status != OrderStatusEnum.Active.ToString())
                throw new BusinessRuleViolationException(StaticValues.OrderDateChangeError, StaticValues.OrderDateChangeMessage);

            if (oldOrder.OrderDeliveryDate.Date < orderDate.Date)
                throw new BusinessRuleViolationException(StaticValues.OrderDeliveryDateError, StaticValues.OrderDeliveryDateMessage);

            oldOrder.OrderDate = orderDate;
            oldOrder.Note = "Updated Order Date";
            EntityEntry<Order> entity = _context.Attach(oldOrder);
            entity.State = EntityState.Modified;
            var result = await _context.SaveChangesAsync();
            await _customerAccountStatementRespository.UpdateAdvanceDate(oldOrder.Id, orderDate);
            return result;
        }
        public async Task<int> UpdateOrderDescription(int orderDetailId, string description)
        {
            var oldOrder = await _context.OrderDetails.Where(x => !x.IsCancelled && !x.IsDeleted && x.Id == orderDetailId).FirstOrDefaultAsync();

            if (oldOrder == null)
            {
                throw new BusinessRuleViolationException(StaticValues.OrderNotFoundError, StaticValues.OrderNotFoundMessage);
            }

            oldOrder.Description = description;
            EntityEntry<OrderDetail> entity = _context.Attach(oldOrder);
            entity.State = EntityState.Modified;
            var result = await _context.SaveChangesAsync();
            return result;
        }
        public async Task<List<Order>> GetOrderNoByContactNo(string contactNo)
        {
            return await _context.Orders.Include(x => x.Customer).Include(x => x.OrderDetails).Where(x => x.Customer.Contact1.Contains(contactNo)).ToListAsync();
        }
        public async Task<int> EditOrder(OrderEditRequest orderEditRequest)
        {
            var oldOrder = await _context.Orders
                .Include(x => x.OrderDetails)
                .Include(x => x.AccountStatements)
                .Where(x => !x.IsCancelled && !x.IsDeleted && x.Id == orderEditRequest.OrderId).FirstOrDefaultAsync();
            if (oldOrder == null)
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            if (oldOrder.Status != OrderStatusEnum.Active.ToString())
                throw new BusinessRuleViolationException(StaticValues.OrderNotInActiveStateError, StaticValues.OrderNotInActiveStateMessage);
            oldOrder.CustomerId = orderEditRequest.CustomerId;
            oldOrder.OrderDeliveryDate = orderEditRequest.DeliveryDate;
            oldOrder.PaymentMode = orderEditRequest.PaymentMode;
            oldOrder.OrderDetails.ForEach(res =>
            {
                res.DeliveredDate = orderEditRequest.DeliveryDate;
            });
            oldOrder.AccountStatements.ForEach(res =>
            {
                if (res.PaymentDate.Date == oldOrder.OrderDate.Date && res.Reason == AccountStatementReasonEnum.AdvancedPaid.ToString())
                {
                    res.PaymentMode = orderEditRequest.PaymentMode;
                }
            });
            var ent = _context.Attach(oldOrder);
            ent.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
        public async Task<int> ChangeStatusIfAllKandooraDeliverd(int orderId)
        {
            var oldOrder = await _context.Orders
                .Include(x => x.OrderDetails.Where(y => y.Status == OrderStatusEnum.Delivered.ToString()))
                .Where(x => !x.IsCancelled && !x.IsDeleted && x.Id == orderId).FirstOrDefaultAsync();
            if (oldOrder == null)
                return 0;
            if (oldOrder.Qty == oldOrder.OrderDetails.Count)
            {
                oldOrder.Status = OrderStatusEnum.Delivered.ToString();
                oldOrder.Note = "Change Status - " + OrderStatusEnum.Delivered.ToString();
                var ent = _context.Attach(oldOrder);
                ent.State = EntityState.Modified;
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
        public async Task<int> GetNextTaxInvoiceNo(DateTime DeliveryDate)
        {
            int defaultTaxInvoiceNo = _configuration.GetValue<int>("TaxInvoiceNoStartFrom");
            var defaultTaxInvoiceNoStartFrom = _configuration.GetValue<DateTime>("TaxInvoiceNoStartIfDeliveryDateStartFrom");
            var currTaxInvoiceNo = await _context.Orders.MaxAsync(x => x.TaxInvoiceNo);
            if (DeliveryDate <= defaultTaxInvoiceNoStartFrom)
                return currTaxInvoiceNo + 1;
            else
            {
                if (currTaxInvoiceNo >= defaultTaxInvoiceNo)
                    return currTaxInvoiceNo + 1;
                return defaultTaxInvoiceNo;
            }
        }

        public async Task<List<OrderDetail>> GetUsedModelByContactNo(string contactNo)
        {
            var result= await _context.OrderDetails
                                    .Include(x => x.Order)
                                    .ThenInclude(x => x.Customer)
                                    .Include(x=>x.DesignSample)
                                    .Where(x => x.Order.Customer.Contact1 == contactNo && x.ModelNo!=null && x.ModelNo != "null" && (x.ModelNo != "" || x.DesignSampleId > 0))
                                    .ToListAsync();
            return result;
        }

        public async Task<Dictionary<string, int>> GetOrderCountByContactNo(List<string> contactNos)
        {
            var result = await _context.Orders
                 .Include(x => x.Customer)
                 .Where(x => contactNos.Contains(x.Customer.Contact1))
                 .ToListAsync();
              var grpResult=result
                .GroupBy(x => x.Customer.Contact1);

            Dictionary<string, int> orderCounts = new Dictionary<string, int>();
            foreach (var item in grpResult)
            {
                orderCounts.Add(item.Key, item.Count());
            }
            return orderCounts;
        }

        public async Task<OrderDetail> GetOrderDetails(int orderDetailId)
        {
            return await _context
                        .OrderDetails
                        .Include(x => x.Order)
                        .ThenInclude(x=>x.Employee)
                        .Include(x => x.DesignSample)
                        .Where(x => x.Id == orderDetailId)
                        .FirstOrDefaultAsync();
        }
    }
}
