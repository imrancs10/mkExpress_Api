
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;

namespace MKExpress.API.Services.Interfaces
{
    public interface IMasterDataService : ICrudService<MasterDataRequest, MasterDataResponse>
    {
        Task<List<MasterDataResponse>> GetByMasterDataType(string masterDataType);
        Task<List<MasterDataResponse>> GetByMasterDataTypes(List<string> masterDataTypes);
    }
}
