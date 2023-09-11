using AutoMapper;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Customer;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class CustomerAccountStatementService : ICustomerAccountStatementService
    {
        private readonly ICustomerAccountStatementRespository _customerStatementRepository;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public CustomerAccountStatementService(ICustomerAccountStatementRespository customerStatementRepository, IMapper mapper, IOrderRepository orderRepository)
        {
            _customerStatementRepository = customerStatementRepository;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<int> AddAdvancePayment(List<CustomerAdvancePaymentRequest> customerAdvancePaymentRequests)
        {
            List<CustomerAccountStatement> customerAccountStatement = _mapper.Map<List<CustomerAccountStatement>>(customerAdvancePaymentRequests);
           var result = await _customerStatementRepository.AddAdvancePayment(customerAccountStatement);
            if (result > 0)
            {
                await _orderRepository.UpdateOrderAdvancePayment(customerAccountStatement);
            }
            return result;
        }

        public async Task<List<CustomerAdvancePaymentResponse>> GetAdvancePaymentStatement(int orderId)
        {
            return _mapper.Map<List<CustomerAdvancePaymentResponse>>(await _customerStatementRepository.GetAdvancePaymentStatement(orderId));
        }

        public async Task<decimal> GetCustomerBalanceAmount(int customerId, int excludeOrderId)
        {
            return await _customerStatementRepository.GetCustomerBalanceAmount(customerId, excludeOrderId);
        }

        public async Task<List<CustomerPreviousAmountStatementResponse>> GetCustomerPreviousAmountStatement(string contactNo)
        {
            return _mapper.Map<List<CustomerPreviousAmountStatementResponse>>(await _customerStatementRepository.GetCustomerPreviousAmountStatement(contactNo));
        }

        public async Task<List<CustomerAccountStatementResponse>> GetPaymentSummary(DateTime fromDate, DateTime toDate, string paymentType, string paymentMode)
        {
            var res= _mapper.Map<List<CustomerAccountStatementResponse>>(await _customerStatementRepository.GetPaymentSummary(fromDate,toDate,paymentType,paymentMode));
            res.ForEach(ele =>
            {
                ele.Order.AccountStatements = null;
            });
            return res;
        }

        public async Task<int> UpdateStatement(CustomerAdvancePaymentRequest accountStatement)
        {
            var request = _mapper.Map<CustomerAccountStatement>(accountStatement);
            return await _customerStatementRepository.UpdateStatement(request);
        }
    }
}
