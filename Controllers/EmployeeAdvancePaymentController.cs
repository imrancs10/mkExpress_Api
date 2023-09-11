using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.Web.API.Controllers
{
    [ApiController]
    public class EmployeeAdvancePaymentController : ControllerBase
    {
        private readonly IEmployeeAdvancePaymentService _employeeAdvancePaymentService;

        public EmployeeAdvancePaymentController(IEmployeeAdvancePaymentService employeeAdvancePaymentService)
        {
            _employeeAdvancePaymentService = employeeAdvancePaymentService;
        }

        [ProducesResponseType(typeof(EmployeeAdvancePaymentResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(EmployeeAdvancePaymentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.EmployeeAdvancePaymentPath)]
        public async Task<EmployeeAdvancePaymentResponse> AddEmployeeAdvancePayment([FromBody] EmployeeAdvancePaymentRequest employeeAdvancePaymentRequest)
        {
            return await _employeeAdvancePaymentService.Add(employeeAdvancePaymentRequest);
        }

        [ProducesResponseType(typeof(EmployeeAdvancePaymentResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(EmployeeAdvancePaymentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.EmployeeAdvancePaymentPath)]
        public async Task<EmployeeAdvancePaymentResponse> UpdateEmployeeAdvancePayment([FromBody] EmployeeAdvancePaymentRequest employeeAdvancePaymentRequest)
        {
            return await _employeeAdvancePaymentService.Update(employeeAdvancePaymentRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<EmployeeAdvancePaymentResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(PagingResponse<EmployeeAdvancePaymentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.EmployeeAdvancePaymentPath)]
        public async Task<PagingResponse<EmployeeAdvancePaymentResponse>> GetAllEmployeeAdvancePayment([FromQuery] PagingRequest pagingRequest)
        {
            return await _employeeAdvancePaymentService.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.EmployeeAdvancePaymentDeletePath)]
        public async Task<int> DeleteEmployeeAdvancePayment([FromRoute] int id)
        {
            return await _employeeAdvancePaymentService.Delete(id);
        }

        [ProducesResponseType(typeof(EmployeeAdvancePaymentResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(EmployeeAdvancePaymentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.EmployeeAdvancePaymentByIdPath)]
        public async Task<EmployeeAdvancePaymentResponse> GetEmployeeAdvancePayment([FromRoute] int id)
        {
            return await _employeeAdvancePaymentService.Get(id);
        }

        [ProducesResponseType(typeof(PagingResponse<EmployeeAdvancePaymentResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(PagingResponse<EmployeeAdvancePaymentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.EmployeeAdvancePaymentSearchPath)]
        public async Task<PagingResponse<EmployeeAdvancePaymentResponse>> SearchEmployeeAdvancePayment([FromQuery] SearchPagingRequest pagingRequest)
        {
            return await _employeeAdvancePaymentService.Search(pagingRequest);
        }

        [ProducesResponseType(typeof(List<EmployeeEMIPaymentResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<EmployeeEMIPaymentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.EmployeeAdvancePaymentByEmployeeIdPath)]
        public async Task<List<EmployeeEMIPaymentResponse>> GetEmiDetailsByEmployeeId([FromRoute] int employeeId)
        {
            return await _employeeAdvancePaymentService.GetByEmployeeId(employeeId);
        }

        [ProducesResponseType(typeof(List<EmployeeAdvancePaymentResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<EmployeeAdvancePaymentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.EmployeeAdvancePaymentGetStatementPath)]
        public async Task<List<EmployeeAdvancePaymentResponse>> GetStatements([FromRoute] int empId)
        {
            return await _employeeAdvancePaymentService.GetStatement(empId);
        }
    }
}
