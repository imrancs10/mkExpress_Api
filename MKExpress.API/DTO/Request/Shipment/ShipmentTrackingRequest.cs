using MKExpress.API.DTO.Base;

namespace MKExpress.API.DTO.Request
{
    public class ShipmentTrackingRequest:BaseRequest
    {
        public Guid ShipmentId { get; set; }
        public string Activity { get; set; }
        public string? Comment1 { get; set; }
        public string? Comment2 { get; set; }
        public string? Comment3 { get; set; }
        public Guid? CommentBy { get; set; }
    }
}
