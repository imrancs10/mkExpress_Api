using Microsoft.EntityFrameworkCore;
using MKExpress.API.Constants;
using MKExpress.API.Data;
using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MKExpressDbContext _context;
        private readonly decimal vat = 5;
        public AccountRepository(MKExpressDbContext context)
        {
            _context = context;
        }


        public async Task<AccountSummaryReportResponse> GetAccountSummary(DateTime fromDate, DateTime toDate)
        {
            List<CustomerAccountStatement> statement = await _context.CustomerAccountStatements
                .Where(x => !x.IsDeleted && x.PaymentDate.Date >= fromDate.Date && x.PaymentDate.Date <= toDate)
                .ToListAsync();
            var response = new AccountSummaryReportResponse();
            if (statement.Count() == 0)
                return response;
            decimal cashAdvAmount = CalculateAmount(statement, AccountStatementReasonEnum.AdvancedPaid, PaymentModeEnum.Cash.ToString());
            decimal visaAdvAmount = CalculateAmount(statement, AccountStatementReasonEnum.AdvancedPaid, PaymentModeEnum.Visa.ToString());
            decimal cancelledAmount = (decimal)statement.Where(x => x.Reason == AccountStatementReasonEnum.SubOrderCancelled.ToString() || x.Reason == AccountStatementReasonEnum.OrderCancelled.ToString()).Sum(x => x.Credit);
            decimal deletedAmount = (decimal)statement.Where(x => x.Reason == AccountStatementReasonEnum.SubOrderDeleted.ToString() || x.Reason == AccountStatementReasonEnum.OrderDeleted.ToString()).Sum(x => x.Credit);
            decimal cashDeliveryAmount = CalculateAmount(statement, AccountStatementReasonEnum.PaymentReceived, PaymentModeEnum.Cash.ToString());
            decimal visaDeliveryAmount = CalculateAmount(statement, AccountStatementReasonEnum.PaymentReceived, PaymentModeEnum.Visa.ToString());
            decimal refundAmount = CalculateAmount(statement, AccountStatementReasonEnum.Refund, "All");
            decimal bookingCashAmount = (decimal)statement.Where(x => x.Reason == AccountStatementReasonEnum.OrderCreated.ToString() && x.PaymentMode != null && x.PaymentMode.ToUpper() == PaymentModeEnum.Cash.ToString().ToUpper()).Sum(x => x.Debit);
            decimal bookingVisaAmount = (decimal)statement.Where(x => x.Reason == AccountStatementReasonEnum.OrderCreated.ToString() && x.PaymentMode != null && x.PaymentMode.ToUpper() == PaymentModeEnum.Visa.ToString().ToUpper()).Sum(x => x.Debit);
            decimal bookingAmount = bookingCashAmount + bookingVisaAmount - cancelledAmount - deletedAmount;

            response.TotalAdvanceAmount = cashAdvAmount + visaAdvAmount;
            response.TotalAdvanceCashAmount = cashAdvAmount;
            response.TotalAdvanceVisaAmount = visaAdvAmount;
            response.TotalAdvanceVatAmount = CalculatePercentage(response.TotalAdvanceAmount, vat);
            response.TotalBookingAmount = (bookingAmount - cancelledAmount - deletedAmount);
            response.TotalCancelledAmount = cancelledAmount;
            response.TotalDeletedAmount = deletedAmount;
            response.TotalBookingVatAmount = CalculatePercentage(response.TotalBookingAmount, vat);
            response.TotalDeliveryAmount = cashDeliveryAmount + visaDeliveryAmount;
            response.TotalDeliveryCashAmount = cashDeliveryAmount;
            response.TotalDeliveryVisaAmount = visaDeliveryAmount;
            response.TotalRefundAmount = refundAmount;
            response.TotalBookingCashAmount = bookingCashAmount;
            response.TotalBookingVisaAmount = bookingVisaAmount;
            response.TotalBalanceVatAmount = CalculatePercentage((response.TotalBookingAmount - response.TotalAdvanceAmount - response.TotalDeliveryAmount), vat);
            return response;
        }

        private decimal CalculateAmount(List<CustomerAccountStatement> data, AccountStatementReasonEnum reason, string paymentMode)
        {
            var lapsedData = data.Where(x => x.Reason == AccountStatementReasonEnum.SubOrderDeleted.ToString() || x.Reason == AccountStatementReasonEnum.SubOrderCancelled.ToString() || x.Reason == AccountStatementReasonEnum.OrderDeleted.ToString() || x.Reason == AccountStatementReasonEnum.OrderCancelled.ToString()).ToList();
            var lapsedDataIds = lapsedData.Where(x => paymentMode?.ToUpper()=="ALL" || (x.PaymentMode == null && x.PaymentMode?.ToUpper() == paymentMode?.ToUpper()) ||(x.PaymentMode != null && x.PaymentMode?.ToUpper() == paymentMode?.ToUpper())).Select(x => x.OrderId).ToList();
            var result = data.Where(x => x.Reason == reason.ToString() && (paymentMode?.ToUpper() == "ALL" || (x.PaymentMode != null && x.PaymentMode?.ToUpper() == paymentMode?.ToUpper())) && !lapsedDataIds.Contains(x.OrderId));
            return (decimal)result.Sum(x => x.Credit);
        }
        private decimal CalculatePercentage(decimal amount, decimal percent)
        {
            return (amount / 100) * percent;
        }
    }
}
