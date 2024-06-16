using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Enums;
using MKExpress.API.Models;

namespace MKExpress.API.Repository
{
    public interface IShipmentRepository
    {
        Task<Shipment> CreateShipment(Shipment shipment);
        Task<bool> IsShipmentExists(string shipmentNo);
        Task<PagingResponse<Shipment>> GetAllShipment(PagingRequest pagingRequest);
        Task<Shipment> GetShipment(Guid id);
        Task<List<Shipment>> ValidateShipment(List<string> shipmentNo);
        Task<Shipment?> ValidateShipmentStatus(string shipmentNo, ShipmentStatusEnum status);
        Task<List<Shipment>> GetShipment(List<Guid> ids);
        Task<bool> AssignForPickup(List<AssignShipmentMember> requests);
        Task<bool> AssignForDelivery(List<AssignShipmentMember> requests);
        Task<bool> UpdateShipmentStatus(List<Guid> shipmentIds,ShipmentStatusEnum newStatus,string comment = "");
        Task<List<Shipment>> GetShipmentByUser(string userName, ShipmentStatusEnum shipment, ShipmentStatusEnum shipmentStatus);
        Task<bool> HoldShipment(List<Guid> requests);
        Task<PagingResponse<Shipment>> SearchShipment(SearchShipmentRequest requests);
    }
}
