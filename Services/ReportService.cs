using AutoMapper;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Customer;
using MKExpress.API.Dto.Response.Report;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;
        public ReportService(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task<List<BillingTaxReportResponse>> BillingCancelTaxReport(DateTime fromDate, DateTime toDate)
        {
            return _mapper.Map<List<BillingTaxReportResponse>>(await _reportRepository.BillingCancelTaxReport(fromDate, toDate));
        }

        public async Task<List<BillingTaxReportResponse>> BillingTaxReport(DateTime fromDate, DateTime toDate)
        {
            var res= _mapper.Map<List<BillingTaxReportResponse>>(await _reportRepository.BillingTaxReport(fromDate, toDate));
            res.ForEach(acc =>
            {
                acc.Order.AccountStatements = null;
            });
            return res.OrderBy(x=>x.Order.TaxInvoiceNo).ToList();
        }

        public async Task<DailyStatusResponse> DailyStatusReport(DateTime date)
        {
            return await _reportRepository.DailyStatusReport(date);
        }

        public async Task<List<DailyWorkStatementResponse>> DailyWorkStatement(DailyWorkStatementPagingRequest pagingRequest)
        {
           return await _reportRepository.DailyWorkStatement(pagingRequest);
        }

        public async Task<PagingResponse<KandooraExpenseResponse>> EachKandooraExpenseReport(KandooraExpensePagingRequest pagingRequest)
        {
            return await _reportRepository.EachKandooraExpenseReport(pagingRequest);
        }

        public async Task<List<WorkerPerformanceReportResponse>> GetWorkerPerformance(int workType, DateTime fromDate, DateTime toDate)
        {
            return await _reportRepository.GetWorkerPerformance(workType, fromDate, toDate);
        }

        public async Task<List<DailyWorkStatementResponse>> SearchDailyWorkStatement(DailyWorkStatementPagingRequest pagingRequest)
        {
            return await _reportRepository.SearchDailyWorkStatement(pagingRequest);
        }

        public async Task<PagingResponse<KandooraExpenseResponse>> SearchEachKandooraExpenseReport(SearchPagingRequest pagingRequest)
        {
            return await _reportRepository.SearchEachKandooraExpenseReport(pagingRequest);
        }
    }
}
