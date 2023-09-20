using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class ShipmentImage:BaseModel
    {
        public int ShipmentId { get; set; }
        public string Url { get; set; }

        [ForeignKey("ShipmentId")]
        public Shipment Shipment { get; set; }

    }
}
