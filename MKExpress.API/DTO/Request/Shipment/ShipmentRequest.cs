namespace MKExpress.API.DTO.Request
{
    public class ShipmentRequest
    {
        public string ShipmentNumber { get; set; }
        public Guid CustomerId { get; set; }
        public string UniqueRefNo { get; set; }
        public string Status { get; set; }
        public string StatusReason { get; set; }
        public int FailedDelivery { get; set; }
        public decimal? CODAmount { get; set; }
        public string? Location { get; set; }
        public ShipmentDetailRequest ShipmentDetail { get; set; }
    }
}
