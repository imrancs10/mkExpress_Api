using MKExpress.API.Models;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface IMasterDataTypeRepository : ICrudRepository<MasterDataType>
    {
        Task<bool> IsMasterDataTypeExist(string masterDataType);
    }
}
