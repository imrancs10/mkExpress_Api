using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface IKandooraExpenseService
    {
        Task<EachKandooraExpenseHeadResponse> AddExpenseHead(EachKandooraExpenseHeadRequest eachKandooraExpenseHead);
        Task<List<EachKandooraExpenseResponse>> AddExpense(List<EachKandooraExpenseRequest> eachKandooraExpense);
        Task<int> DeleteExpenseHead(int id);
        Task<EachKandooraExpenseHeadResponse> GetExpenseHead(int id);
        Task<PagingResponse<EachKandooraExpenseHeadResponse>> GetAllExpenseHead(PagingRequest pagingRequest);
        Task<PagingResponse<EachKandooraExpenseResponse>> GetAllExpense(PagingRequest pagingRequest);
        Task<PagingResponse<EachKandooraExpenseHeadResponse>> SearchExpenseHead(SearchPagingRequest searchPagingRequest);
        Task<EachKandooraExpenseHeadResponse> UpdateExpenseHead(EachKandooraExpenseHeadRequest eachKandooraExpenseHead);
        Task<decimal> GetTotalOfExpense();
    }
}
