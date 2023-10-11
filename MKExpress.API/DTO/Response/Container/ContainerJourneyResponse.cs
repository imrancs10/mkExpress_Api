using MKExpress.API.DTO.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.DTO.Response
{
    public class ContainerJourneyResponse : BaseResponse
    {
        public string StationName { get; set; }
        public Guid ContainerId { get; set; }
        public Guid StationId { get; set; }
        public DateTime ArrivalAt { get; set; }
        public DateTime DepartureOn { get; set; }
        public int SequenceNo { get; set; }
        public bool IsSourceStation { get; set; }
        public bool IsDestinationStation { get; set; }
        public Guid? CheckInById { get; set; }
        public Guid? CheckOutById { get; set; }
        public string? CheckInBy { get; set; }
        public string? CheckOutBy { get; set; }
    }
}
