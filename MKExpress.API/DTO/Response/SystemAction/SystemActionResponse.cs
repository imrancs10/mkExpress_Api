namespace MKExpress.API.DTO.Response
{
    public class SystemActionResponse
    {

        public DateTime? ShipmentCreatedOn { get; set; }
        public DateTime? ActionDate { get; set; }
        public string? PaymentMode { get; set; }
        public string ActionByName { get; set; }
        public string ActionType { get; set; }
        public string ShipmentNumber { get; set; }
        public string UniqueReferanceNumber { get; set; }
        public string CurrentStatus { get; set; }
        public string Station { get; set; }
        public DateTime? ReceiveOn { get; set; }
        public string ShipperName { get; set; } 
        public string ConsigneeName { get; set; }
        public string ConsigneeCity { get; set; }
        public string? CustomerName { get; set; }
        public decimal? CodAmount { get; set; }
        public string Comment1 { get; set; }
        public string Comment2 { get; set; }
        public string Comment3 { get; set; }
    }
}
