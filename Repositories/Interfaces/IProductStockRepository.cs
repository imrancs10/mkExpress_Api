using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface IProductStockRepository
    {
        Task<int> AddNewStocks(List<ProductStock> productStocks);
        Task<int> IncreaseStocks(List<ProductStock> productStocks);
        Task<int> DecreaseStocks(List<ProductStock> productStocks);
        Task<int> DecreaseStocks(int productStockId,int qty);
        Task<int> IncreaseStocks(int productStockId, int qty);
        Task<List<ProductStock>> GetCrystals();
        Task<int> SaveOrderUsedCrystals(List<OrderUsedCrystal> orderUsedCrystals);
        Task<List<OrderUsedCrystal>> GetOrderUsedCrystals(int OrderDetailId);
        Task<List<OrderUsedCrystal>> GetOrderUsedCrystalsByEmployee(int empId);

    }
}
