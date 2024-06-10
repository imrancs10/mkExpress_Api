using MKExpress.API.Models;

namespace MKExpress.API.Repository
{
    public interface IMasterDataRepository : ICrudRepository<MasterData>
    {
        Task<List<MasterData>> GetByMasterDataType(string masterDataType);
        Task<List<MasterData>> GetByMasterDataTypes(List<string> masterDataTypes);
    }
}