using MKExpress.API.DTO.Base;
using MKExpress.API.DTO.Request;
using MKExpress.API.Models;

namespace MKExpress.API.DTO.Response
{
    public class ContainerResponse:BaseResponse
    {
        public string Journey { get; set; }
        public string ContainerType { get; set; }
        public int TotalShipments { get; set; }
        public int ContainerNo { get; set; }
        public Guid ContainerTypeId { get; set; }
        public DateTime? ClosedOn { get; set; }
        public Guid? ClosedBy { get; set; }
        public bool IsClosed { get; set; }
        public Guid JourneyId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? ClosedByMember { get; set; }
        public List<ContainerDetailResponse> ContainerDetails { get; set; }
        public List<ContainerJourneyResponse> ContainerJourneys { get; set; }
        public List<ContainerTrackingResponse> ContainerTrackings { get; set; }
    }
}
