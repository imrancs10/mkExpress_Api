using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface ICrystalPurchaseService:ICrudService<CrystalPurchaseRequest, CrystalPurchaseResponse>
    {
        Task<int> GetCrystalPurchaseNo();
    }
}
