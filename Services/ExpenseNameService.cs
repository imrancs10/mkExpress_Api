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
    public class ExpenseNameService : IExpenseNameService
    {
        private readonly IExpenseNameRepository _expenseNameRepository;
        private readonly IMapper _mapper;
        public ExpenseNameService(IExpenseNameRepository expenseNameRepository, IMapper mapper)
        {
            _expenseNameRepository = expenseNameRepository;
            _mapper = mapper;
        }
        public async Task<ExpenseNameResponse> Add(ExpenseNameRequest request)
        {
            ExpenseName expenseName = _mapper.Map<ExpenseName>(request);
            return _mapper.Map<ExpenseNameResponse>(await _expenseNameRepository.Add(expenseName));
        }

        public async Task<int> Delete(int id)
        {
            return await _expenseNameRepository.Delete(id);
        }

        public async Task<ExpenseNameResponse> Get(int id)
        {
            return _mapper.Map<ExpenseNameResponse>(await _expenseNameRepository.Get(id));
        }

        public async Task<PagingResponse<ExpenseNameResponse>> GetAll(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<ExpenseNameResponse>>(await _expenseNameRepository.GetAll(pagingRequest));
        }

        public async Task<PagingResponse<ExpenseNameResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<ExpenseNameResponse>>(await _expenseNameRepository.Search(searchPagingRequest));
        }

        public async Task<ExpenseNameResponse> Update(ExpenseNameRequest request)
        {
            ExpenseName expenseName = _mapper.Map<ExpenseName>(request);
            return _mapper.Map<ExpenseNameResponse>(await _expenseNameRepository.Update(expenseName));
        }
    }
}
