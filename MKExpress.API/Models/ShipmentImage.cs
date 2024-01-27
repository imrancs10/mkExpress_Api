using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class ShipmentImage:BaseModel
    {
        public Guid ShipmentId { get; set; }
        public Guid? TrackingId { get; set; }
        public string? Url { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string? FileType { get; set; }
        public string? ModuleName { get; set; }
        public string? Remark { get; set; }
        public int? SequenceNo { get; set; }

        [ForeignKey("ShipmentId")]
        public Shipment? Shipment { get; set; }

        [ForeignKey("TrackingId")]
        public ShipmentTracking? ShipmentTracking { get; set; }

    }
}
