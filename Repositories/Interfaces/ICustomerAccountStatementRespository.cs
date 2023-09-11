using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface ICustomerAccountStatementRespository
    {
        Task<decimal> GetCustomerBalanceAmount(int customerId, int excludeOrderId);
        Task<int> AddAdvancePayment(List<CustomerAccountStatement> customerAdvancePaymentRequests);
        Task<List<CustomerPreviousAmountStatementResponse>> GetCustomerPreviousAmountStatement(string contactNo);
        Task<List<CustomerAccountStatement>> GetAdvancePaymentStatement(int orderId);
        Task<int> UpdateAdvanceDate(int orderId, DateTime advanceDate);
        Task<int> UpdateStatement(CustomerAccountStatement accountStatement);
        Task<List<CustomerAccountStatement>> GetPaymentSummary(DateTime fromDate, DateTime toDate, string paymentType, string paymentMode);
    }
}
