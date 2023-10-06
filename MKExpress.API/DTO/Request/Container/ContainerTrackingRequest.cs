namespace MKExpress.API.DTO.Request
{
    public class ContainerTrackingRequest
    {
        public Guid ContainerId { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid StationId { get; set; }
        public string Code { get; set; }
    }
}
