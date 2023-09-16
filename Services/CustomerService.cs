using AutoMapper;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerResponse> Add(CustomerRequest customerRequest)
        {
            Customer customer = _mapper.Map<Customer>(customerRequest);
            return _mapper.Map<CustomerResponse>(await _customerRepository.Add(customer));
        }

        public async Task<int> Delete(int customerId)
        {
            await ValidateCustomer(customerId);
            return await _customerRepository.Delete(customerId);
        }

        public async Task<CustomerResponse> Get(int customerId)
        {
            CustomerResponse customerResponse = _mapper.Map<CustomerResponse>(await _customerRepository.Get(customerId));

            if (customerResponse == null)
                throw new BusinessRuleViolationException(StaticValues.CustomerNotFoundError, StaticValues.CustomerNotFoundMessage);
            return customerResponse;
        }

        public async Task<PagingResponse<CustomerResponse>> GetAll(PagingRequest pagingRequest)
        {
            PagingResponse<CustomerResponse> result = _mapper.Map<PagingResponse<CustomerResponse>>(await _customerRepository.GetAll(pagingRequest));
           
            return result;
        }

        public async Task<List<CustomerResponse>> GetCustomers(string contactNo)
        {
            return _mapper.Map<List<CustomerResponse>>(await _customerRepository.GetCustomers(contactNo));
        }

        public async Task<PagingResponse<CustomerResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            var result= _mapper.Map<PagingResponse<CustomerResponse>>(await _customerRepository.Search(searchPagingRequest));
           
            return result;
        }

        public async Task<CustomerResponse> Update(CustomerRequest customerRequest)
        {
            Customer customer = _mapper.Map<Customer>(customerRequest);
            return _mapper.Map<CustomerResponse>(await _customerRepository.Update(customer));
        }

        private async Task ValidateCustomer(int customerId)
        {
            Customer customer = await _customerRepository.Get(customerId);
            if (customer == null || customer.Id != customerId)
            {
                throw new BusinessRuleViolationException(StaticValues.CustomerNotFoundError, StaticValues.CustomerNotFoundMessage);
            }
        }
    }
}
