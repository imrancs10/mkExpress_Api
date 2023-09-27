using MKExpress.API.Models;

namespace MKExpress.API.Repository.IRepository
{
    public interface IShipmentRepository
    {
        Task<Shipment> CreateShipment(Shipment shipment);
    }
}
