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
    public class DropdownController : ControllerBase
    {
        private readonly IDropdownService _dropdownService;
        public DropdownController(IDropdownService dropdownService)
        {
            _dropdownService = dropdownService;
        }

        [ProducesResponseType(typeof(List<DropdownResponse<EmployeeResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.DropdownEmployeePath)]
        public async Task<List<DropdownResponse<EmployeeResponse>>> GetEmployeeDropdown([FromQuery] DropdownRequest dropdownRequest)
        {
            return await _dropdownService.Employee(dropdownRequest.SearchTerm);
        }

        [ProducesResponseType(typeof(List<DropdownResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.DropdownCustomerPath)]
        public async Task<List<DropdownResponse>> GetCustomerDropdown([FromQuery] DropdownRequest dropdownRequest)
        {
            return await _dropdownService.Customers(dropdownRequest.SearchTerm);
        }

        [ProducesResponseType(typeof(List<DropdownResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.DropdownCustomerOrderPath)]
        public async Task<List<DropdownResponse>> GetCustomerOrderDropdown()
        {
            return await _dropdownService.CustomerOrders();
        }

        [ProducesResponseType(typeof(List<DropdownResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.DropdownJobTitlePathPath)]
        public async Task<List<DropdownResponse>> GetJobTitleDropdown()
        {
            return await _dropdownService.JobTitle();
        }

        [ProducesResponseType(typeof(List<DropdownResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.DropdownProductPath)]
        public async Task<List<DropdownResponse>> GetProductsDropdown()
        {
            return await _dropdownService.Products();
        }

        [ProducesResponseType(typeof(List<DropdownResponse<SupplierResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.DropdownSupplierPath)]
        public async Task<List<DropdownResponse<SupplierResponse>>> GetSuppliersDropdown()
        {
            return await _dropdownService.Suppliers();
        }

        [ProducesResponseType(typeof(List<DropdownResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.DropdownDesignCategoryPath)]
        public async Task<List<DropdownResponse>> GetDesignCategoryDropdown()
        {
            return await _dropdownService.DesignCategory();
        }

        [ProducesResponseType(typeof(List<DropdownResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.DropdownOrderDetailNosPath)]
        public async Task<List<DropdownResponse>> OrderDetailNos(bool excludeDelivered=false)
        {
            return await _dropdownService.OrderDetailNos(excludeDelivered);
        }

        [ProducesResponseType(typeof(List<DropdownResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.DropdownWorkTypePath)]
        public async Task<List<DropdownResponse>> WorkTypes()
        {
            return await _dropdownService.WorkTypes();
        }
    }
}
