using MKExpress.API.DTO.Base;
using MKExpress.API.Models;

namespace MKExpress.API.DTO.Response
{
    public class ShipmentResponse:BaseResponse
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
        public DateTime? CreatedAt { get; set; }
        public DateTime? ScheduleDeliveryDate { get; set; }
        public ShipmentDetailResponse ShipmentDetail { get; set; }
        public string CustomerName { get; set; }

    }
}
