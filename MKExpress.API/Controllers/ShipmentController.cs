using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Enums;
using MKExpress.API.Services;

namespace MKExpress.API.Controllers
{
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService _shipmentService;
        public ShipmentController(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }

        [ProducesResponseType(typeof(ShipmentResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ShipmentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.ShipmentPath)]
        public async Task<ShipmentResponse> CreateShipment([FromBody] ShipmentRequest request)
        {
            return await _shipmentService.CreateShipment(request);
        }

        [ProducesResponseType(typeof(PagingResponse<ShipmentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ShipmentSearchPath)]
        public async Task<PagingResponse<ShipmentResponse>> SearchShipment([FromQuery] SearchShipmentRequest request)
        {
            return await _shipmentService.SearchShipment(request);
        }

        [ProducesResponseType(typeof(PagingResponse<ShipmentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ShipmentPath)]
        public async Task<PagingResponse<ShipmentResponse>> GetAllShipment([FromQuery] PagingRequest request)
        {
            return await _shipmentService.GetAllShipment(request);
        }

        [ProducesResponseType(typeof(ShipmentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ShipmentByIdPath)]
        public async Task<ShipmentResponse> GetShipment([FromRoute] Guid id)
        {
            return await _shipmentService.GetShipment(id);
        }

        [ProducesResponseType(typeof(List<ShipmentTrackingResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ShipmentTrackingByShipmentIdPath)]
        public async Task<List<ShipmentTrackingResponse>> GetTrackingByShipmentId([FromRoute] Guid id)
        {
            return await _shipmentService.GetTrackingByShipmentId(id);
        }

        [ProducesResponseType(typeof(List<ShipmentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ShipmentByIdsPath)]
        public async Task<List<ShipmentResponse>> GetShipment([FromRoute] string id)
        {
            return await _shipmentService.GetShipment(id);
        }

        [ProducesResponseType(typeof(ShipmentValidateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.ShipmentValidatePath)]
        public async Task<ShipmentValidateResponse> ValidateContainerShipment([FromBody] List<string> shipmentNo, [FromRoute] Guid id)
        {
            return await _shipmentService.ValidateContainerShipment(shipmentNo, id);
        }

        [ProducesResponseType(typeof(ShipmentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ShipmentValidateThirdPartyPath)]
        public async Task<ShipmentResponse> ValidateThirdPartyShipment([FromQuery] string shipmentNo)
        {
            return await _shipmentService.ValidateThirdPartyShipment(shipmentNo);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.ShipmentAssignPickupPath)]
        public async Task<bool> AssignForPickup([FromBody] List<AssignForPickupRequest> request)
        {
            return await _shipmentService.AssignForPickup(request);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.ShipmentAssignDeliveryPath)]
        public async Task<bool> AssignForDelivery([FromBody] List<AssignForPickupRequest> request)
        {
            return await _shipmentService.AssignForDelivery(request);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.ShipmentHoldPath)]
        public async Task<bool> HoldShipment([FromBody] List<Guid> request)
        {
            return await _shipmentService.HoldShipment(request);
        }

        [ProducesResponseType(typeof(List<ShipmentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ShipmentByUserNamePath)]
        public async Task<List<ShipmentResponse>> GetShipments([FromQuery] string userId, [FromQuery] ShipmentStatusEnum shipment, [FromQuery] ShipmentStatusEnum shipmentStatus)
        {
            return await _shipmentService.GetShipments(userId, shipment, shipmentStatus);
        }

        [ProducesResponseType(typeof(ShipmentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ShipmentValidateStatusPath)]
        public async Task<ShipmentResponse?> ValidateShipmentStatus([FromQuery] string shipmentNo, [FromQuery] string status)
        {
            return await _shipmentService.ValidateShipmentStatus(shipmentNo, status);
        }
    }
}
