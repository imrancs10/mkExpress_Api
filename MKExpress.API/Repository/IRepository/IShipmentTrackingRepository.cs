﻿using MKExpress.API.Models;

namespace MKExpress.API.Repository
{
    public interface IShipmentTrackingRepository
    {
        Task<ShipmentTracking> AddTracking(ShipmentTracking shipmentTracking);
        Task<bool> AddTracking(List<ShipmentTracking> shipmentTrackings);
        Task<List<ShipmentTracking>> GetTrackingByShipmentId(Guid shipmentId);
    }
}
