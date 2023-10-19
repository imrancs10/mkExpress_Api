using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class ThirdPartyShipment : BaseModel
    {
        public Guid ThirdPartyCourierCompanyId { get; set; }
        public Guid ShipmentId { get; set; }
        public DateTime AssignAt { get; set; }
        public Guid AssignById { get; set; }
        public string? ThirdPartyShipmentNo { get; set; }

        [ForeignKey("ThirdPartyCourierCompanyId")]
        public ThirdPartyCourierCompany ThirdPartyCourierCompany { get; set; }

        [ForeignKey("ShipmentId")]
        public Shipment Shipment { get; set; }

        [ForeignKey("AssignById")]
        public Member AssignBy { get; set; }
    }
}
