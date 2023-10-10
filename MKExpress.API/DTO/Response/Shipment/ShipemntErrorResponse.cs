namespace MKExpress.API.DTO.Response
{
    public class ShipmentErrorResponse
    {
        public string ShipmentNo { get; set; }
        public bool IsValid { get; set; }
        public string Error { get; set; }
    }
}
