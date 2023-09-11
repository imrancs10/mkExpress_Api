using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Request.Expense;
using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Expense;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface IExpenseService 
    {
        Task<int> GetExpenseNo();
        Task<ExpenseResponse> Add(ExpenseRequest request);
        Task<ExpenseResponse> Update(ExpenseRequest request);
        Task<int> Delete(int id);
        Task<ExpenseResponse> Get(int id);
        Task<PagingResponse<ExpenseResponse>> GetAll(ExpensePagingRequest pagingRequest);
        Task<PagingResponse<ExpenseResponse>> Search(ExpenseSearchPagingRequest searchPagingRequest);
        Task<List<HeadWiseExpenseSumResponse>> GetHeadWiseExpenseSum(DateTime fromDate, DateTime toDate);
    }

    public interface IExpenseNameService : ICrudService<ExpenseNameRequest, ExpenseNameResponse>
    {
    }

    public interface IExpenseTypeService : ICrudService<MasterDataTypeRequest, MasterDataTypeResponse>
    {
    }
}
