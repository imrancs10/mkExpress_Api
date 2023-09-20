using MKExpress.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface IMasterDataRepository : ICrudRepository<MasterData>
    {
        Task<List<MasterData>> GetByMasterDataType(string masterDataType);
        Task<List<MasterData>> GetByMasterDataTypes(List<string> masterDataTypes);
    }
}