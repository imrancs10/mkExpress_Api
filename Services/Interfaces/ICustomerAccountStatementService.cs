using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Customer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface ICustomerAccountStatementService
    {
        Task<decimal> GetCustomerBalanceAmount(int customerId, int excludeOrderId);
        Task<int> AddAdvancePayment(List<CustomerAdvancePaymentRequest> customerAdvancePaymentRequests);
        Task<List<CustomerPreviousAmountStatementResponse>> GetCustomerPreviousAmountStatement(string contactNo);
        Task<List<CustomerAdvancePaymentResponse>> GetAdvancePaymentStatement(int orderId);
        Task<int> UpdateStatement(CustomerAdvancePaymentRequest accountStatement);
        Task<List<CustomerAccountStatementResponse>> GetPaymentSummary(DateTime fromDate, DateTime toDate, string paymentType, string paymentMode);
    }
}
