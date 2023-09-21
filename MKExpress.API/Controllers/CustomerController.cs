using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Services.Interfaces;

namespace SalehGarib.API.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
       public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
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
        public async Task<int> DeleteCustomer([FromRoute(Name = "id")] Guid customerId)
        {
            return await _customerService.Delete(customerId);
        }

        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.CustomerByIdPath)]
        public async Task<CustomerResponse> GetCustomer([FromRoute] Guid id)
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

    }
}
