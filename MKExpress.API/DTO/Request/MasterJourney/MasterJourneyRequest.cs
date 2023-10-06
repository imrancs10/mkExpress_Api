using MKExpress.API.DTO.Base;

namespace MKExpress.API.DTO.Request
{
    public class MasterJourneyRequest:BaseRequest
    {
        public Guid FromStationId { get; set; }
        public Guid ToStationId { get; set; }
        public List<MasterJourneyDetailRequest> MasterJourneyDetails { get; set; }
    }
}
