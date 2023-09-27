using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;

namespace MKExpress.API.Services.IServices
{
    public interface IShipmentService
    {
        Task<ShipmentResponse> CreateShipment(ShipmentRequest shipment);
    }
}
