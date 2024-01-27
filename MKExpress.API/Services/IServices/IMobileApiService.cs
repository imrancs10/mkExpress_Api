using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;

namespace MKExpress.API.Services.IServices
{
    public interface IMobileApiService
    {
        Task<List<ShipmentResponse>> GetShipmentByMember(Guid memberId, ShipmentStatusEnum shipmentStatus);
        Task<bool> MarkReadyForPickup(Guid memberId, Guid shipmentId);
        Task<bool> MarkPickupDone(Guid memberId, Guid shipmentId); 
        Task<bool> MarkPickupFailed(MarkPickupStatusRequest request);
        Task<bool> MarkPickupReschedule(MarkPickupStatusRequest request);
    }
}
