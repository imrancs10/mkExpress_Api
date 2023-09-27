using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;
using MKExpress.API.Services.IServices;

namespace MKExpress.API.Controllers
{
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService _shipmentService;
        public ShipmentController(IShipmentService shipmentService)
        {
            _shipmentService= shipmentService;
        }

        [ProducesResponseType(typeof(ShipmentResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ShipmentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.ShipmentPath)]
        public async Task<ShipmentResponse> CreateShipment(ShipmentRequest request)
        {
            return await _shipmentService.CreateShipment(request);
        }
    }
}
