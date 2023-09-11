using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface IMasterDataService : ICrudService<MasterDataRequest, MasterDataResponse>
    {
        Task<List<MasterDataResponse>> GetByMasterDataType(string masterDataType);
        Task<List<MasterDataResponse>> GetByMasterDataTypes(List<string> masterDataTypes);
    }
}
