using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class AssignShipmentMember : BaseModel
    {
        public Guid MemberId { get; set; }
        public Guid ShipmentId { get; set; }
        public string? Status { get; set; }
        public Guid AssignById { get; set; }
        public DateTime AssignAt { get; set; }

        [ForeignKey("MemberId")]
        public Member Member { get; set; }

        [ForeignKey("ShipmentId")]
        public Shipment Shipment { get; set; }

        [ForeignKey("AssignById")]
        public Member AssignBy { get; set; }

    }
}
