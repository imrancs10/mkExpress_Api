using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.Web.API.Dto.Response;
using MKExpress.Web.API.Dto.Response.Crystal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface ICrystalTrackingOutRepository
    {
        Task<int> Add(CrystalTrackingOut crystalTrackingOut);
        Task<int> Delete(int Id);
        Task<int> DeleteDetail(int Id);
        Task<int> AddNote(int Id,string note);
        Task<List<KandooraWiseExpenseResponse>> GetKandooraWiseExpense(List<int> orderDetailIds);
        Task<CrystalTrackingOut> Get(int Id);
        Task<List<CrystalTrackingOut>> GetByOrderDetailId(int Id);
        Task<List<CrystalTrackingOutDetail>> GetCrystalConsumedDetails(CrystalStockPagingRequest pagingRequest);
        Task<PagingResponse<CrystalTrackingOut>> GetAll(PagingRequest pagingRequest);
        Task<PagingResponse<CrystalTrackingOut>> Search(SearchPagingRequest searchPagingRequest);
        Task<List<CrystalTrackingOut>> GetOrderUsedCrystalsByEmployee(int empId,int month,int year);
        Task<List<CrystalUsedInOrderResponse>> GetOrderUsedCrystalsByReleaseDateAndCrystalId(int crystalId, DateTime ReleaseDate);
        Task<List<CrystalUsedInOrderResponse>> GetOrderUsedCrystalsByReleaseDateAndCrystalId(int crystalId, DateTime fromDate,DateTime toDate);
    }
}
