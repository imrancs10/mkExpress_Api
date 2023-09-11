using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface IProductStockService
    {
        Task<List<OrderCrystalResponse>> GetCrystals();
        Task<int> SaveOrderUsedCrystals(List<OrderUsedCrystalRequest> orderUsedCrystals);
        Task<List<OrderUsedCrystalResponse>> GetOrderUsedCrystals(int OrderDetailId);
    }
}
