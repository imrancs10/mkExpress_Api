using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface ICrystalPurchaseRepository:ICrudRepository<CrystalPurchase>
    {
        Task<int> GetCrystalPurchaseNo();
        Task<Dictionary<int,int>> GetPurchaseCrystalCounts(DateTime fromDate, DateTime toDate);
    }
}
