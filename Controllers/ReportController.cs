using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Customer;
using MKExpress.API.Dto.Response.Report;
using MKExpress.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Controllers
{
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly ICustomerAccountStatementService _customerAccountStatementService;
        public ReportController(IReportService reportService, ICustomerAccountStatementService customerAccountStatementService)
        {
            _reportService = reportService;
            _customerAccountStatementService = customerAccountStatementService;
        }

        [ProducesResponseType(typeof(List<WorkerPerformanceReportResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ReportWorkerPerformancePath)]
        public async Task<List<WorkerPerformanceReportResponse>> GetCustomer([FromQuery] int workType,DateTime fromDate,[FromQuery] DateTime toDate)
        {
            return await _reportService.GetWorkerPerformance(workType,fromDate,toDate);
        }

        [ProducesResponseType(typeof(DailyStatusResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ReportDailyStatusPath)]
        public async Task<DailyStatusResponse> DailyStatusReport([FromQuery] DateTime date)
        {
            return await _reportService.DailyStatusReport(date);
        }

        [ProducesResponseType(typeof(List<BillingTaxReportResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ReportBillingTaxPath)]
        public async Task<List<BillingTaxReportResponse>> BillingTaxReport(DateTime fromDate, [FromQuery] DateTime toDate)
        {
            return await _reportService.BillingTaxReport(fromDate, toDate);
        }

        [ProducesResponseType(typeof(List<BillingTaxReportResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ReportBillingCancelTaxPath)]
        public async Task<List<BillingTaxReportResponse>> BillingCancelTaxReport(DateTime fromDate, [FromQuery] DateTime toDate)
        {
            return await _reportService.BillingCancelTaxReport(fromDate, toDate);
        }

        [ProducesResponseType(typeof(List<BillingTaxReportResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ReportPaymentSummaryPath)]
        public async Task<List<CustomerAccountStatementResponse>> CustomerPaymentSummary(DateTime fromDate, [FromQuery] DateTime toDate,[FromQuery] string paymentType,[FromQuery] string paymentMode)
        {
            return await _customerAccountStatementService.GetPaymentSummary(fromDate, toDate,paymentType,paymentMode);
        }

        [ProducesResponseType(typeof(PagingResponse<KandooraExpenseResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ReportEachKandooraExpenseReportPath)]
        public async Task<PagingResponse<KandooraExpenseResponse>> EachKandooraExpenseReport([FromQuery] KandooraExpensePagingRequest pagingRequest)
        {
            return await _reportService.EachKandooraExpenseReport(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<KandooraExpenseResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ReportSearchEachKandooraExpenseReportPath)]
        public async Task<PagingResponse<KandooraExpenseResponse>> SearchEachKandooraExpenseReport([FromQuery] SearchPagingRequest pagingRequest)
        {
            return await _reportService.SearchEachKandooraExpenseReport(pagingRequest);
        }

        [ProducesResponseType(typeof(List<DailyWorkStatementResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ReportDailyWorkStatementReportPath)]
        public async Task<List<DailyWorkStatementResponse>> DailyStatusReport([FromQuery] DailyWorkStatementPagingRequest pagingRequest)
        {
            return await _reportService.DailyWorkStatement(pagingRequest);
        }

        [ProducesResponseType(typeof(List<DailyWorkStatementResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ReportSearchDailyWorkStatementReportPath)]
        public async Task<List<DailyWorkStatementResponse>> SeachDailyStatusReport([FromQuery] DailyWorkStatementPagingRequest pagingRequest)
        {
            return await _reportService.SearchDailyWorkStatement(pagingRequest);
        }

    }
}
