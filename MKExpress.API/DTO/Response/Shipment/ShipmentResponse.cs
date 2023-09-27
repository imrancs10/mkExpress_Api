using MKExpress.API.DTO.Request;

namespace MKExpress.API.DTO.Response
{
    public class ShipmentResponse
    {
        public string ShipmentNumber { get; set; }
        public Guid CustomerId { get; set; }
        public string UniqueRefNo { get; set; }
        public int FailedDelivery { get; set; }
        public decimal? CODAmount { get; set; }
        public string? Location { get; set; }
        public List<ShipmentDetailResponse> ShipmentDetails { get; set; }
    }
}
