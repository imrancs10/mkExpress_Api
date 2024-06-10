using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;

namespace MKExpress.API.Services
{
    public interface IAppSettingService:ICrudService<AppSettingRequest,AppSettingResponse>
    {
        Task<List<AppSettingGroupResponse>> GetAllAppSettingGroup();
    }
}
