using AutoMapper;
using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repository;

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

        public async Task<int> Delete(Guid customerId)
        {
            await ValidateCustomer(customerId);
            return await _customerRepository.Delete(customerId);
        }

        public async Task<CustomerResponse> Get(Guid customerId)
        {
            CustomerResponse customerResponse = _mapper.Map<CustomerResponse>(await _customerRepository.Get(customerId));

            if (customerResponse == null)
                throw new BusinessRuleViolationException(StaticValues.ErrorType_CustomerNotFoundError, StaticValues.Error_CustomerNotFoundError);
            return customerResponse;
        }

        public async Task<PagingResponse<CustomerResponse>> GetAll(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<CustomerResponse>>(await _customerRepository.GetAll(pagingRequest));
        }

        public async Task<List<CustomerResponse>> GetCustomers(string contactNo)
        {
            return _mapper.Map<List<CustomerResponse>>(await _customerRepository.GetCustomers(contactNo));
        }

        public async Task<List<DropdownResponse>> GetCustomersDropdown()
        {
            return await _customerRepository.GetCustomersDropdown();
        }

        public async Task<PagingResponse<CustomerResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<CustomerResponse>>(await _customerRepository.Search(searchPagingRequest));
            
        }

        public async Task<CustomerResponse> Update(CustomerRequest customerRequest)
        {
            Customer customer = _mapper.Map<Customer>(customerRequest);
            return _mapper.Map<CustomerResponse>(await _customerRepository.Update(customer));
        }

        private async Task ValidateCustomer(Guid customerId)
        {
            Customer customer = await _customerRepository.Get(customerId);
            if (customer == null || customer.Id != customerId)
            {
                throw new BusinessRuleViolationException(StaticValues.ErrorType_CustomerNotFoundError, StaticValues.Error_CustomerNotFoundError);
            }
        }
    }
}
