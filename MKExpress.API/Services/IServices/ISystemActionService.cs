using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;

namespace MKExpress.API.Services
{
    public interface ISystemActionService
    {
        Task<PagingResponse<SystemActionResponse>> GetSystemActions(SystemActionRequest request);
    }
}
