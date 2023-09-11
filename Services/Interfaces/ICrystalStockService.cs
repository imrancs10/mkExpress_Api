using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface ICrystalStockService
    {
        Task<List<CrystalStockResponse>> GetCrystalStockAlert(CrystalStockPagingRequest pagingRequest);
        Task<PagingResponse<CrystalStockResponseExt>> GetCrystalStockDetails(CrystalStockPagingRequest pagingRequest);
        Task<CrystalStockResponse> GetCrystalStockDetail(int Id);
        Task<int> UpdateStock(CrystalStockRequest crystalStock,string reason);
        Task<PagingResponse<CrystalStockResponse>> SearchCrystalStockAlert(SearchPagingRequest searchPagingRequest);
        Task<PagingResponse<CrystalStockResponseExt>> SearchCrystalStockDetails(SearchPagingRequest searchPagingRequest);
    }
}
