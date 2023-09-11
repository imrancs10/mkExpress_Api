using MKExpress.API.Models;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface IPurchaseEntryRepository : ICrudRepository<PurchaseEntry>
    {
        Task<int> GetPurchaseNo();
    }
}
