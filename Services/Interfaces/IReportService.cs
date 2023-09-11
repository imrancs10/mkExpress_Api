using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Report;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface IReportService
    {
        Task<List<WorkerPerformanceReportResponse>> GetWorkerPerformance(int workType, DateTime fromDate, DateTime toDate);
        Task<DailyStatusResponse> DailyStatusReport(DateTime date);
        Task<List<BillingTaxReportResponse>> BillingTaxReport(DateTime fromDate, DateTime toDate);
        Task<List<BillingTaxReportResponse>> BillingCancelTaxReport(DateTime fromDate, DateTime toDate);
        Task<PagingResponse<KandooraExpenseResponse>> EachKandooraExpenseReport(KandooraExpensePagingRequest pagingRequest);
        Task<PagingResponse<KandooraExpenseResponse>> SearchEachKandooraExpenseReport(SearchPagingRequest pagingRequest);
        Task<List<DailyWorkStatementResponse>> DailyWorkStatement(DailyWorkStatementPagingRequest pagingRequest);
        Task<List<DailyWorkStatementResponse>> SearchDailyWorkStatement(DailyWorkStatementPagingRequest pagingRequest);
    }
}
