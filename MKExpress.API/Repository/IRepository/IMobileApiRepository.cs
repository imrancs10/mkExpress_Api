using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.Enums;
using MKExpress.API.Models;

namespace MKExpress.API.Repository
{
    public interface IMobileApiRepository
    {
        Task<List<Shipment>> GetShipmentByMember(ShipmentStatusEnum shipmentStatus);
        Task<bool> MarkReadyForPickup(Guid shipmentId);
        Task<bool> MarkPickupDone(Guid shipmentId);
        Task<bool> MarkPickupFailed(MarkPickupStatusRequest request);
        Task<bool> MarkPickupReschedule(MarkPickupStatusRequest request);
    }
}
