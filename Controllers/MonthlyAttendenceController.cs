using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.Web.API.Controllers
{
    [ApiController]
    public class MonthlyAttendenceController : ControllerBase
    {
        private readonly IMonthlyAttendenceService _monthlyAttendenceService;
        public MonthlyAttendenceController(IMonthlyAttendenceService monthlyAttendenceService)
        {
            _monthlyAttendenceService = monthlyAttendenceService;
        }

        [ProducesResponseType(typeof(MonthlyAttendenceResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MonthlyAttendenceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.MonthlyAttendencePath)]
        public async Task<MonthlyAttendenceResponse> AddMonthlyAttendence([FromBody] MonthlyAttendenceRequest monthlyAttendenceRequest)
        {
            return await _monthlyAttendenceService.Add(monthlyAttendenceRequest);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.MonthlyDailyAttendencePath)]
        public async Task<int> AddUpdateDailyAttendence([FromBody] List<MonthlyAttendenceRequest> monthlyAttendenceRequest)
        {
            return await _monthlyAttendenceService.AddUpdateDailyAttendence(monthlyAttendenceRequest);
        }

        [ProducesResponseType(typeof(MonthlyAttendenceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.MonthlyAttendencePath)]
        public async Task<MonthlyAttendenceResponse> UpdateMonthlyAttendence([FromBody] MonthlyAttendenceRequest monthlyAttendenceRequest)
        {
            return await _monthlyAttendenceService.Update(monthlyAttendenceRequest);
        }

        [ProducesResponseType(typeof(List<MonthlyAttendenceResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MonthlyGetDailyAttendencePath)]
        public async Task<List<MonthlyAttendenceResponse>> GetDailyAttendence([FromQuery] DateTime attendenceDate)
        {
            return await _monthlyAttendenceService.GetDailyAttendence(attendenceDate);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.MonthlyAttendenceDeletePath)]
        public async Task<int> DeleteMonthlyAttendence([FromRoute(Name = "id")] int monthlyAttendenceId)
        {
            return await _monthlyAttendenceService.Delete(monthlyAttendenceId);
        }

        [ProducesResponseType(typeof(MonthlyAttendenceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MonthlyAttendenceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MonthlyAttendenceByIdPath)]
        public async Task<MonthlyAttendenceResponse> GetMonthlyAttendence([FromRoute] int id)
        {
            return await _monthlyAttendenceService.Get(id);
        }

        [ProducesResponseType(typeof(MonthlyAttendenceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MonthlyAttendenceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MonthlyAttendenceByEmpIdMonthYearPath)]
        public async Task<MonthlyAttendenceResponse> GetMonthlyAttendence([FromRoute] int employeeId, [FromRoute] int month, [FromRoute] int year)
        {
            return await _monthlyAttendenceService.GetByEmpIdMonthYear(employeeId, month, year);
        }

        [ProducesResponseType(typeof(PagingResponse<MonthlyAttendenceResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PagingResponse<MonthlyAttendenceResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MonthlyAttendenceByMonthYearPath)]
        public async Task<PagingResponse<MonthlyAttendenceResponse>> GetMonthlyAttendence([FromQuery] PagingRequest pagingRequest, [FromRoute] int month, [FromRoute] int year)
        {
            return await _monthlyAttendenceService.GetByMonthYear(pagingRequest, month, year);
        }

        [ProducesResponseType(typeof(MonthlyAttendenceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MonthlyAttendenceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MonthlyAttendenceByEmpIdPath)]
        public async Task<PagingResponse<MonthlyAttendenceResponse>> GetMonthlyAttendenceByEmpId([FromRoute] int employeeId, [FromQuery] PagingRequest pagingRequest)
        {
            return await _monthlyAttendenceService.GetByEmpId(employeeId, pagingRequest);
        }

        [ProducesResponseType(typeof(MonthlyAttendenceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PagingResponse<MonthlyAttendenceResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MonthlyAttendencePath)]
        public async Task<PagingResponse<MonthlyAttendenceResponse>> GetAllMonthlyAttendence([FromQuery] PagingRequest pagingRequest)
        {
            return await _monthlyAttendenceService.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(MonthlyAttendenceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MonthlyAttendenceSearchPath)]
        public async Task<PagingResponse<MonthlyAttendenceResponse>> SearchMonthlyAttendence([FromQuery] SearchPagingRequest searchPagingRequest)
        {
            return await _monthlyAttendenceService.Search(searchPagingRequest);
        }
    }
}
