using AutoMapper;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Request.Expense;
using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Expense;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;
        public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }

        public async Task<ExpenseResponse> Add(ExpenseRequest request)
        {
            request.CompanyId = request.CompanyId == 0 ? null : request.CompanyId;
            var expense = _mapper.Map<Expense>(request);
            return _mapper.Map<ExpenseResponse>(await _expenseRepository.Add(expense));
        }

        public async Task<int> Delete(int id)
        {
            return await _expenseRepository.Delete(id);
        }

        public async Task<ExpenseResponse> Get(int id)
        {
            return _mapper.Map<ExpenseResponse>(await _expenseRepository.Get(id));
        }

        public async Task<PagingResponse<ExpenseResponse>> GetAll(ExpensePagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<ExpenseResponse>>(await _expenseRepository.GetAll(pagingRequest));
        }

        public async Task<int> GetExpenseNo()
        {
            return await _expenseRepository.GetExpenseNo();
        }

        public async Task<List<HeadWiseExpenseSumResponse>> GetHeadWiseExpenseSum(DateTime fromDate, DateTime toDate)
        {
            return await _expenseRepository.GetHeadWiseExpenseSum(fromDate, toDate);
        }

        public async Task<PagingResponse<ExpenseResponse>> Search(ExpenseSearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<ExpenseResponse>>(await _expenseRepository.Search(searchPagingRequest));
        }

        public async Task<ExpenseResponse> Update(ExpenseRequest request)
        {
            var expense = _mapper.Map<Expense>(request);
            return _mapper.Map<ExpenseResponse>(await _expenseRepository.Update(expense));
        }
    }
}
