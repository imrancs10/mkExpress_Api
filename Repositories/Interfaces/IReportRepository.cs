using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Request.Orders;
using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Report;
using MKExpress.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface IReportRepository
    {
        Task<List<WorkerPerformanceReportResponse>> GetWorkerPerformance(int workType,DateTime fromDate,DateTime toDate);
        Task<DailyStatusResponse> DailyStatusReport(DateTime date);
        Task<List<CustomerAccountStatement>> BillingTaxReport(DateTime fromDate,DateTime toDate);
        Task<List<CustomerAccountStatement>> BillingCancelTaxReport(DateTime fromDate, DateTime toDate);
        Task<PagingResponse<KandooraExpenseResponse>> EachKandooraExpenseReport(KandooraExpensePagingRequest pagingRequest);
        Task<PagingResponse<KandooraExpenseResponse>> SearchEachKandooraExpenseReport(SearchPagingRequest pagingRequest);
        Task<List<DailyWorkStatementResponse>> DailyWorkStatement(DailyWorkStatementPagingRequest pagingRequest);
        Task<List<DailyWorkStatementResponse>> SearchDailyWorkStatement(DailyWorkStatementPagingRequest pagingRequest);
    }
}
