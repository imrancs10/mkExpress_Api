using MKExpress.API.DTO.Base;
using MKExpress.API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.DTO.Response
{
    public class ContainerResponse:BaseResponse
    {
        public int ContainerNo { get; set; }
        public string ContainerType { get; set; }
        public int TotalShipments { get; set; }
        public DateTime? ClosedOn { get; set; }
        public string Journey { get; set; }
        public Guid? ClosedBy { get; set; }
        public string ClosedByMember { get; set; }
        public List<ContainerDetailResponse> ContainerDetails { get; set; }
        public List<ContainerJourneyResponse> ContainerJourneys { get; set; }
    }
}
