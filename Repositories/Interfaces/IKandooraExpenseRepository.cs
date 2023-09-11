using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface IKandooraExpenseRepository
    {
        Task<EachKandooraExpenseHead> AddExpenseHead(EachKandooraExpenseHead eachKandooraExpenseHead);
        Task<List<EachKandooraExpense>> AddExpense(List<EachKandooraExpense> eachKandooraExpense);
        Task<int> DeleteExpenseHead(int id);
        Task<EachKandooraExpenseHead> GetExpenseHead(int id);
        Task<PagingResponse<EachKandooraExpenseHead>> GetAllExpenseHead(PagingRequest pagingRequest);
        Task<PagingResponse<EachKandooraExpense>> GetAllExpense(PagingRequest pagingRequest);
        Task<PagingResponse<EachKandooraExpenseHead>> SearchExpenseHead(SearchPagingRequest searchPagingRequest);
        Task<EachKandooraExpenseHead> UpdateExpenseHead(EachKandooraExpenseHead eachKandooraExpenseHead);
        Task<decimal> GetTotalOfExpense();
    }
}
