using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Request.Employee;
using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Employee;
using MKExpress.API.Services.Interfaces;
using MKExpress.Web.API.Dto.Response.Employee;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Controllers
{
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [ProducesResponseType(typeof(EmployeeResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(EmployeeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.EmployeePath)]
        public async Task<EmployeeResponse> AddEmployee([FromBody] EmployeeRequest EmployeeRequest)
        {
            return await _employeeService.Add(EmployeeRequest);
        }

        [ProducesResponseType(typeof(EmployeeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.EmployeePath)]
        public async Task<EmployeeResponse> UpdateEmployee([FromBody] EmployeeRequest EmployeeRequest)
        {
            return await _employeeService.Update(EmployeeRequest);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.EmployeeDeletePath)]
        public async Task<int> DeleteEmployee([FromRoute(Name = "id")] int EmployeeId)
        {
            return await _employeeService.Delete(EmployeeId);
        }

        [ProducesResponseType(typeof(EmployeeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmployeeResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.EmployeeByIdPath)]
        public async Task<EmployeeResponse> GetEmployee([FromRoute] int id)
        {
            return await _employeeService.Get(id);
        }

        [ProducesResponseType(typeof(PagingResponse<EmployeeResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PagingResponse<EmployeeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.EmployeePath)]
        public async Task<PagingResponse<EmployeeResponse>> GetAllEmployee([FromQuery] EmployeePagingRequest pagingRequest)
        {
            return await _employeeService.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<EmployeeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.EmployeeSearchPath)]
        public async Task<PagingResponse<EmployeeResponse>> SearchEmployee([FromQuery] EmployeeSearchPagingRequest searchPagingRequest)
        {
            return await _employeeService.Search(searchPagingRequest);
        }

        [ProducesResponseType(typeof(List<EmployeeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.EmployeeByJobIdPath)]
        public async Task<List<EmployeeResponse>> GetEmployeeByJobIds([FromQuery] List<int> jobIds)
        {
            return await _employeeService.GetEmployeeByJobIds(jobIds);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.EmployeeSendAlertPath)]
        public async Task<int> SendExpireDocumentsAlertEmail([FromRoute] int empId)
        {
            return await _employeeService.SendExpireDocumentsAlertEmail(empId);
        }

        [ProducesResponseType(typeof(List<EmployeeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<EmployeeResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.EmployeeAllActiveDeactivePath)]
        public async Task<List<EmployeeResponse>> GetAllActiveDeactiveEmployee([FromQuery] bool allFixed = false)
        {
            return await _employeeService.GetAllActiveDeactiveEmployee(allFixed);
        }

        [ProducesResponseType(typeof(List<EmployeeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<EmployeeResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.EmployeeSearchAllPath)]
        public async Task<List<EmployeeResponse>> GetAllActiveDeactiveEmployeeSearch([FromQuery] string searchTearm, [FromQuery] bool allFixed = false)
        {
            return await _employeeService.GetAllActiveDeactiveEmployeeSearch(searchTearm, allFixed);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.EmployeeActiveDeactivePath)]
        public async Task<bool> ActiveDeactiveEmployee([FromRoute] int empId, [FromRoute] bool isActive)
        {
            return await _employeeService.ActiveDeactiveEmployee(empId, isActive);
        }

        [ProducesResponseType(typeof(List<EmployeeSalarySlipResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.EmployeeGetSalarySlipPath)]
        public async Task<List<EmployeeSalarySlipResponse>> GetEmployeeSalarySlip([FromQuery] int empId, [FromQuery] int month, [FromQuery] int year)
        {
            return await _employeeService.GetEmployeeSalarySlip(empId,month,year);
        }

        [ProducesResponseType(typeof(List<EmployeeSalaryLedgerResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.EmployeeGetSalaryLedgerPath)]
        public async Task<List<EmployeeSalaryLedgerResponse>> GetEmployeeSalaryLedger([FromQuery] int jobTitleId, [FromQuery] int month, [FromQuery] int year)
        {
            return await _employeeService.GetEmployeeSalaryLedger(jobTitleId, month, year);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.EmployeePaySalaryPath)]
        public async Task<int> PayMonthlySalary([FromRoute] int id, [FromRoute] DateTime paidOn,[FromRoute] decimal salary)
        {
            return await _employeeService.PayMonthlySalary(id, paidOn,salary);
        }
    }
}
