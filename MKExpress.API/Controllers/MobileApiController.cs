﻿using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Enums;
using MKExpress.API.Services;

namespace MKExpress.API.Controllers
{
    [ApiController]
    public class MobileApiController : ControllerBase
    {
        private readonly IMobileApiService _mobileApiService;
        public MobileApiController(IMobileApiService mobileApiService)
        {
            _mobileApiService = mobileApiService;
        }

        [ProducesResponseType(typeof(List<ShipmentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MobileAPIGetShipmentByMemberPath)]
        public async Task<List<ShipmentResponse>> GetShipmentByMember([FromQuery] ShipmentStatusEnum shipmentStatus)
        {
            return await _mobileApiService.GetShipmentByMember(shipmentStatus);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.MobileAPIMarkPickupDonePath)]
        public async Task<bool> MarkPickupDone([FromQuery] Guid shipmentId)
        {
            return await _mobileApiService.MarkPickupDone(shipmentId);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.MobileAPIMarkReadyForPickupPath)]
        public async Task<bool> MarkReadyForPickup([FromQuery] Guid shipmentId)
        {
            return await _mobileApiService.MarkReadyForPickup(shipmentId);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.MobileAPIMarkPickupFailedPath)]
        public async Task<bool> MarkReadyForPickup([FromForm] MarkPickupStatusRequest request)
        {
            return await _mobileApiService.MarkPickupFailed(request);
        }
    }
}
