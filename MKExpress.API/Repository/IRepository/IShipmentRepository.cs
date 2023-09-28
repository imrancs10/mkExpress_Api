using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;

namespace MKExpress.API.Repository.IRepository
{
    public interface IShipmentRepository
    {
        Task<Shipment> CreateShipment(Shipment shipment);
        Task<PagingResponse<Shipment>> GetAllShipment(PagingRequest pagingRequest);
        Task<Shipment> GetShipment(Guid id);
    }
}
