using MKExpress.API.Contants;

namespace MKExpress.API.Services.IServices
{
    public interface ICommonService
    {
        bool ValidateThirdPartyShipmentStatus(ShipmentStatusEnum shipmentStatus);
        string ValidateShipmentStatus(ShipmentStatusEnum currentStatus,ShipmentStatusEnum newStatus);
        string ValidateShipmentStatus(string currentStatus, ShipmentStatusEnum newStatus);
    }
}
