using MKExpress.API.Dto.Response;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<AccountSummaryReportResponse> GetAccountSummary(DateTime fromDate, DateTime toDate)
        {
            return await _accountRepository.GetAccountSummary(fromDate, toDate);
        }
    }
}
