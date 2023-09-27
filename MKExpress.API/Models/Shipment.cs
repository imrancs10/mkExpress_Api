using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class Shipment:BaseModel
    {
        public string ShipmentNumber { get; set; }
        public Guid CustomerId { get; set; }
        public string UniqueRefNo { get; set; }
        public string Status { get; set; }
        public string StatusReason { get; set; }
        public int FailedDelivery { get; set; }
        public decimal? CODAmount { get; set; }
        public string? Location { get; set; }
        public int StatusDuration { get; set; }
        public DateTime? SchedulePickupDate { get; set; }
        public DateTime? PickupDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? ScheduleDeliveryDate { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public List<ShipmentDetail> ShipmentDetails { get; set; }

    }
}
