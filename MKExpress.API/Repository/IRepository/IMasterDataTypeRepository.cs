using MKExpress.API.Models;

namespace MKExpress.API.Repository
{
    public interface IMasterDataTypeRepository : ICrudRepository<MasterDataType>
    {
        Task<bool> IsMasterDataTypeExist(string masterDataType);
    }
}
