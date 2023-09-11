using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface ICrystalStockRepository
    {
        Task<int> UpdateStock(CrystalStock crystalStock,string reason);
        Task<int> UpdateStock(List<CrystalStock> crystalStocks);
        Task<List<CrystalStock>> GetCrystalStockAlert(CrystalStockPagingRequest pagingRequest);
        Task<PagingResponse<CrystalStock>> GetCrystalStockDetails(CrystalStockPagingRequest pagingRequest);
        Task<PagingResponse<CrystalStock>> SearchCrystalStockAlert(SearchPagingRequest searchPagingRequest);
        Task<PagingResponse<CrystalStock>> SearchCrystalStockDetails(SearchPagingRequest searchPagingRequest);
        Task<CrystalStock> GetCrystalStockDetail(int Id);
        Task<int> DecreaseStock(List<CrystalStock> crystalStocks);
        Task<int> IncreaseStock(List<CrystalStock> crystalStocks);
        Task<int> IncreaseStock(CrystalStock crystalStock);
        Task<bool> AddNewCrystalInStockWithZeroQty(int crystalId);

    }
}
