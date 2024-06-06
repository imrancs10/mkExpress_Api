using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class ShipmentTracking:BaseModel
    {
        public Guid ShipmentId { get; set; }
        public string Activity { get; set; }
        public string? Comment1 { get; set; }
        public string? Comment2 { get; set; }
        public string? Comment3 { get; set; }
        public Guid? CommentBy { get; set; }

        [ForeignKey("ShipmentId")]
        public Shipment Shipment { get; set; }

        [ForeignKey("CommentBy")]
        public User? CommentByMember { get; set; }

        public List<ShipmentImage> ShipmentImages { get; set; }

    }
}
