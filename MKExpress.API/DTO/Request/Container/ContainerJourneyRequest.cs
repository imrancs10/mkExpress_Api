using MKExpress.API.DTO.Base;

namespace MKExpress.API.DTO.Request
{
    public class ContainerJourneyRequest:BaseRequest
    {
        public Guid ContainerId { get; set; }
        public Guid FromStationId { get; set; }
        public Guid ToStationId { get; set; }
    }
}
