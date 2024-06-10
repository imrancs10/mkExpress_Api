using MKExpress.API.DTO.Base;

namespace MKExpress.API.DTO.Response
{
    public class ShipmentImageResponse:BaseResponse
    {
        public Guid ShipmentId { get; set; }
        public Guid? TrackingId { get; set; }
        public string? Url { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string? FileType { get; set; }
        public string? ModuleName { get; set; }
        public string? Remark { get; set; }
        public int? SequenceNo { get; set; }
    }
}
