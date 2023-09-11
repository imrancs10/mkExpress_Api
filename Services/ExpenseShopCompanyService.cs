using AutoMapper;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Request.Expense;
using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Expense;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class ExpenseShopCompanyService : IExpenseShopCompanyService
    {
        private readonly IMapper _mapper;
        private readonly IExpenseShopCompanyRepository _expenseShopCompanyRepository;
        public ExpenseShopCompanyService(IMapper mapper, IExpenseShopCompanyRepository expenseShopCompanyRepository)
        {
            _expenseShopCompanyRepository = expenseShopCompanyRepository;
            _mapper = mapper;
        }
        public async Task<ExpenseShopCompanyResponse> Add(ExpenseShopCompanyRequest request)
        {
            ExpenseShopCompany expenseShopCompany = _mapper.Map<ExpenseShopCompany>(request);
            return _mapper.Map<ExpenseShopCompanyResponse>(await _expenseShopCompanyRepository.Add(expenseShopCompany));
        }

        public async Task<int> Delete(int id)
        {
            return await _expenseShopCompanyRepository.Delete(id);
        }

        public async Task<ExpenseShopCompanyResponse> Get(int id)
        {
            return _mapper.Map<ExpenseShopCompanyResponse>(await _expenseShopCompanyRepository.Get(id));
        }

        public async Task<PagingResponse<ExpenseShopCompanyResponse>> GetAll(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<ExpenseShopCompanyResponse>>(await _expenseShopCompanyRepository.GetAll(pagingRequest));
        }

        public async Task<PagingResponse<ExpenseShopCompanyResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<ExpenseShopCompanyResponse>>(await _expenseShopCompanyRepository.Search(searchPagingRequest));
        }

        public async Task<ExpenseShopCompanyResponse> Update(ExpenseShopCompanyRequest request)
        {
            ExpenseShopCompany expenseShopCompany = _mapper.Map<ExpenseShopCompany>(request);
            return _mapper.Map<ExpenseShopCompanyResponse>(await _expenseShopCompanyRepository.Update(expenseShopCompany));
        }
    }
}
