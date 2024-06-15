using MKExpress.API.Contants;
using MKExpress.API.Enums;

namespace MKExpress.API.Services
{
    public interface ICommonService
    {
        bool ValidateThirdPartyShipmentStatus(ShipmentStatusEnum shipmentStatus);
        string ValidateShipmentStatus(ShipmentStatusEnum currentStatus,ShipmentStatusEnum newStatus);
        string ValidateShipmentStatus(string currentStatus, ShipmentStatusEnum newStatus);
        string GeneratePasswordHash(string base64String);
    }
}
