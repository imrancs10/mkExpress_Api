using MKExpress.API.Dto.Response;
using System;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AccountSummaryReportResponse> GetAccountSummary(DateTime fromDate, DateTime toDate);
    }
}
