using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Services.Interfaces;

namespace MKExpress.API.Services.IServices
{
    public interface IAppSettingService:ICrudService<AppSettingRequest,AppSettingResponse>
    {
    }
}
