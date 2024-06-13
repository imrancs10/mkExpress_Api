using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;

namespace MKExpress.API.Services
{
    public interface IMobileApiService
    {
        Task<List<ShipmentResponse>> GetShipmentByMember(ShipmentStatusEnum shipmentStatus);
        Task<bool> MarkReadyForPickup(Guid shipmentId);
        Task<bool> MarkPickupDone(Guid shipmentId); 
        Task<bool> MarkPickupFailed(MarkPickupStatusRequest request);
        Task<bool> MarkPickupReschedule(MarkPickupStatusRequest request);
    }
}
