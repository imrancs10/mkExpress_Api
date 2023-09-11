using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface ICrystalPurchaseEmiRepository
    {
        Task<int> Add(IList<CrystalPurchaseInstallment> crystalPurchaseInstallments);
    }
}
