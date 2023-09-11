using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface IExpenseTypeRepository : ICrudRepository<ExpenseType>
    {
    }

    public interface IExpenseNameRepository : ICrudRepository<ExpenseName>
    {
        Task<int> GetExpenseNameByCode(string nameCode);
    }

    public interface IExpenseRepository
    {
        Task<Expense> Add(Expense entity);
        Task<Expense> Update(Expense entity);
        Task<int> Delete(int Id);
        Task<Expense> Get(int Id);
        Task<PagingResponse<Expense>> GetAll(ExpensePagingRequest expensePaging);
        Task<PagingResponse<Expense>> Search(ExpenseSearchPagingRequest searchPagingRequest);
        Task<int> GetExpenseNo();
        Task<int> AddCancelOrderExpense(decimal amount, string remark, string paymentMode="Cash");
        Task<decimal> GetTotalCancelOrderExpenseByDate(DateTime date);
        Task<List<HeadWiseExpenseSumResponse>> GetHeadWiseExpenseSum(DateTime fromDate, DateTime toDate);
    }
}
