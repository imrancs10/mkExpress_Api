using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerAccountStatementService _customerAccountStatementService;
        public CustomerController(ICustomerService customerService, ICustomerAccountStatementService customerAccountStatementService)
        {
            _customerService = customerService;
            _customerAccountStatementService = customerAccountStatementService;
        }

        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.CustomerPath)]
        public async Task<CustomerResponse> AddCustomer([FromBody] CustomerRequest customerRequest)
        {
            return await _customerService.Add(customerRequest);
        }

        [ProducesResponseType(typeof(List<CustomerResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.CustomerByContactNoPath)]
        public async Task<List<CustomerResponse>> GetCustomer([FromQuery] string contactNo)
        {
            return await _customerService.GetCustomers(contactNo);
        }

        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.CustomerPath)]
        public async Task<CustomerResponse> UpdateCustomer([FromBody] CustomerRequest customerRequest)
        {
            return await _customerService.Update(customerRequest);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.CustomerDeletePath)]
        public async Task<int> DeleteCustomer([FromRoute(Name = "id")] int customerId)
        {
            return await _customerService.Delete(customerId);
        }

        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.CustomerByIdPath)]
        public async Task<CustomerResponse> GetCustomer([FromRoute] int id)
        {
            return await _customerService.Get(id);
        }

        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PagingResponse<CustomerResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.CustomerPath)]
        public async Task<PagingResponse<CustomerResponse>> GetAllCustomer([FromQuery] PagingRequest pagingRequest)
        {
            return await _customerService.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.CustomerSearchPath)]
        public async Task<PagingResponse<CustomerResponse>> SearchCustomer([FromQuery] SearchPagingRequest searchPagingRequest)
        {
            return await _customerService.Search(searchPagingRequest);
        }

        [ProducesResponseType(typeof(List<CustomerPreviousAmountStatementResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.CustomerGetPreAmountStatementPath)]
        public async Task<List<CustomerPreviousAmountStatementResponse>> SearchCustomer([FromQuery] string contactNo)
        {
            return await _customerAccountStatementService.GetCustomerPreviousAmountStatement(contactNo);
        }

        [ProducesResponseType(typeof(List<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.CustomerAddAdvanceAmountPath)]
        public async Task<int> AddCustomerAdvancePayment([FromBody] List<CustomerAdvancePaymentRequest> customerAdvancePaymentRequests)
        {
            return await _customerAccountStatementService.AddAdvancePayment(customerAdvancePaymentRequests);
        }
    }
}
