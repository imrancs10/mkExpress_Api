using MKExpress.API.Contants;

namespace MKExpress.API.Services.IServices
{
    public interface ICommonService
    {
        Guid GetLoggedInUserId();
        bool ValidateThirdPartyShipmentStatus(ShipmentStatusEnum shipmentStatus);
        string ValidateShipmentStatus(ShipmentStatusEnum currentStatus,ShipmentStatusEnum newStatus);
    }
}
