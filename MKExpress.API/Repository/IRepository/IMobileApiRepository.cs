using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.Models;

namespace MKExpress.API.Repository.IRepository
{
    public interface IMobileApiRepository
    {
        Task<List<Shipment>> GetShipmentByMember(Guid memberId,ShipmentStatusEnum shipmentStatus);
        Task<bool> MarkReadyForPickup(Guid memberId,Guid shipmentId);
        Task<bool> MarkPickupDone(Guid memberId, Guid shipmentId);
        Task<bool> MarkPickupFailed(MarkPickupStatusRequest request);
        Task<bool> MarkPickupReschedule(MarkPickupStatusRequest request);
    }
}
