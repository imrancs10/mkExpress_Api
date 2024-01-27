using MKExpress.API.DTO.Base;
using MKExpress.API.DTO.Response.Image;

namespace MKExpress.API.DTO.Response
{
    public class ShipmentTrackingResponse : BaseResponse
    {
        public Guid ShipmentId { get; set; }
        public string Activity { get; set; }
        public string? Comment1 { get; set; }
        public string? Comment2 { get; set; }
        public string? Comment3 { get; set; }
        public Guid? CommentBy { get; set; }
        public string? CommentByName { get; set; }
        public DateTime CreatedAt { get; set; }
        public ShipmentResponse? Shipment { get; set; }
        public List<ShipmentImageResponse> ShipmentImages { get; set; }

    }
}
