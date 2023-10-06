using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class ContainerDetail:BaseModel
    {
        public Guid ContainerId { get; set; }
        public Guid ShipmentId { get; set; }

        [ForeignKey("ContainerId")]
        public Container Container { get; set; }
        [ForeignKey("ShipmentId")]
        public Shipment Shipment { get; set; }
    }
}
