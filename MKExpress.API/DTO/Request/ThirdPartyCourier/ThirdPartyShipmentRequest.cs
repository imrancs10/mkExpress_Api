using MKExpress.API.DTO.Base;

namespace MKExpress.API.DTO.Request
{
    public class ThirdPartyShipmentRequest : BaseRequest
    {
        public Guid ThirdPartyCourierCompanyId { get; set; }
        public Guid ShipmentId { get; set; }
        public string? ThirdPartyShipmentNo { get; set; }
        public DateTime AssignAt { get; set; }
        public Guid AssignById { get; set; }
    }
}
