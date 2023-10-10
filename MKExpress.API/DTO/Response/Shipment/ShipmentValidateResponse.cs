namespace MKExpress.API.DTO.Response
{
    public class ShipmentValidateResponse
    {
        public List<ShipmentResponse> Shipments { get; set; }
        public List<ShipmentErrorResponse> Errors { get; set; }

    }
}
