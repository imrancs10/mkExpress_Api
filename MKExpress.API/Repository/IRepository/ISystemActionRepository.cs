using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;

namespace MKExpress.API.Repository
{
    public interface ISystemActionRepository
    {
        Task<PagingResponse<ShipmentTracking>> GetSystemActions(SystemActionRequest request);
    }
}
