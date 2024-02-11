namespace MKExpress.API.DTO.Response
{
    public class AssignShipmentMemberResponse
    {
        public string ShipmentNumber { get; set; }
        public Guid? ShipmentId { get; set; }
        public string UniqueRefNo { get; set; }
        public string Status { get; set; }
        public decimal? Weight { get; set; }
        public string Dimension { get; set; }
        public string ShipperName { get; set; }
        public string ShipperAddress1 { get; set; }
        public string? ShipperAddress2 { get; set; }
        public string? ShipperAddress3 { get; set; }
        public string ShipperCity { get; set; }
        public string ConsigneeName { get; set; }
        public string ConsigneeAddress1 { get; set; }
        public string? ConsigneeAddress2 { get; set; }
        public string? ConsigneeAddress3 { get; set; }
        public string? ConsigneeCity { get; set; }
        public string? Customer { get; set; }
        public int? NoOfPiece { get; set; }
        public decimal? CodAmount { get; set; }

    }
}
