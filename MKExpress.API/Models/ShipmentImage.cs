using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class ShipmentImage:BaseModel
    {
        public Guid ShipmentId { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }

        [ForeignKey("ShipmentId")]
        public Shipment Shipment { get; set; }

    }
}
