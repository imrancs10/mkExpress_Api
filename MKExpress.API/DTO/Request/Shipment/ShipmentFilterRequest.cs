namespace MKExpress.API.DTO.Request.Shipment
{
    public class ShipmentFilterRequest
    {
        public int? CustomerId { get; set; }
        public string? Reason { get; set; }
        public string? Status { get; set; }
        public int? CourierId { get; set; }
        public int? StationId { get; set; }
        public int? ConsigneeCityId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? DeliveredFromDate { get; set; }
        public DateTime? DeliveredToDate { get; set; }
        public DateTime? ReceivedFromDate { get; set; }
        public DateTime? ReceivedToDate { get; set; }
        public DateTime? CodFromDate { get; set; }
        public DateTime? CodToDate { get; set; }
        public DateTime? ReturnFromDate { get; set; }
        public DateTime? ReturnToDate { get; set; }
        public string SearchTerm { get; set; }

    }
}
