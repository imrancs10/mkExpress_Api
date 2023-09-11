using MKExpress.API.Dto.Response;
using System;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<AccountSummaryReportResponse> GetAccountSummary(DateTime fromDate, DateTime toDate);
    }
}
