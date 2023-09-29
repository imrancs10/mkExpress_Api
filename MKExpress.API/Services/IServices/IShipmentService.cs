﻿using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;

namespace MKExpress.API.Services.IServices
{
    public interface IShipmentService
    {
        Task<ShipmentResponse> CreateShipment(ShipmentRequest shipment);
        Task<List<ShipmentTrackingResponse>> GetTrackingByShipmentId(Guid shipmentId);
        Task<PagingResponse<ShipmentResponse>> GetAllShipment(PagingRequest pagingRequest);
        Task<ShipmentResponse> GetShipment(Guid id);
        Task<List<ShipmentResponse>> GetShipment(string ids);
    }
}
