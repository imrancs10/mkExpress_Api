using MKExpress.API.Models;

namespace MKExpress.API.Repository.IRepository
{
    public interface IShipmentTrackingRepository
    {
        Task<ShipmentTracking> AddTracking(ShipmentTracking shipmentTracking);
        Task<List<ShipmentTracking>> GetTrackingByShipmentId(Guid shipmentId);
    }
}
