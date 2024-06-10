using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Enums;

namespace MKExpress.API.Services
{
    public interface IShipmentService
    {
        Task<ShipmentResponse> CreateShipment(ShipmentRequest shipment);
        Task<List<ShipmentTrackingResponse>> GetTrackingByShipmentId(Guid shipmentId);
        Task<PagingResponse<ShipmentResponse>> GetAllShipment(PagingRequest pagingRequest);
        Task<ShipmentResponse> GetShipment(Guid id);
        Task<List<ShipmentResponse>> GetShipment(string ids);
        Task<ShipmentValidateResponse> ValidateContainerShipment(List<string> shipmentNo,Guid containerJourneyId);
        Task<ShipmentResponse> ValidateThirdPartyShipment(string shipmentNo);
        Task<bool> AssignForPickup(List<AssignForPickupRequest> requests);
        Task<bool> HoldShipment(List<Guid> requests);
        Task<List<ShipmentResponse>> GetShipments(string userId, ShipmentEnum shipment, ShipmentStatusEnum shipmentStatus);
        Task<ShipmentResponse?> ValidateShipmentStatus(string shipmentNo, string status);
        Task<PagingResponse<ShipmentResponse>> SearchShipment(SearchShipmentRequest requests);
    }
}
