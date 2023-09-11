using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.Web.API.Dto.Request;
using MKExpress.Web.API.Dto.Response.Crystal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface ICrystalTrackingOutService
    {
        Task<int> Add(CrystalTrackingOutRequest crystalTrackingOut);
        Task<int> Delete(int Id); 
        Task<int> DeleteDetail(int Id); 
        Task<int> AddNote(int Id, string note);
        Task<CrystalTrackingOutResponse> Get(int Id);
        Task<List<CrystalTrackingOutResponse>> GetByOrderDetailId(int Id); 
        Task<List<CrystalConsumeDetailResponse>> GetCrystalConsumedDetails(CrystalStockPagingRequest pagingRequest);
        Task<PagingResponse<CrystalTrackingOutResponse>> GetAll(PagingRequest pagingRequest);
        Task<PagingResponse<CrystalTrackingOutResponse>> Search(SearchPagingRequest searchPagingRequest);
        Task<List<CrystalUsedInOrderResponse>> GetOrderUsedCrystalsByReleaseDateAndCrystalId(int crystalId, DateTime releaseDate);
        Task<List<CrystalUsedInOrderResponse>> GetOrderUsedCrystalsByReleaseDateAndCrystalId(int crystalId, DateTime fromDate, DateTime toDate);
    }
}
