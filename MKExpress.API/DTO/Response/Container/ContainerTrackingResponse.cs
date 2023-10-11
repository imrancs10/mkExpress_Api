using MKExpress.API.DTO.Base;
using MKExpress.API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.DTO.Response
{
    public class ContainerTrackingResponse:BaseResponse
    {
        public Guid ContainerJourneyId { get; set; }
        public Guid ContainerId { get; set; }
        public string Code { get; set; }
        public Guid CreatedById { get; set; }
        public string CreatedMember { get; set; }
        public string StationName { get; set; }

    }
}
