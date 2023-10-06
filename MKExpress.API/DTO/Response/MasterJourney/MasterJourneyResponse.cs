using MKExpress.API.DTO.Base;

namespace MKExpress.API.DTO.Response
{
    public class MasterJourneyResponse:BaseResponse
    {
        public Guid FromStationId { get; set; }
        public Guid ToStationId { get; set; }
        public List<MasterJourneyDetailResponse> MasterJourneyDetails { get; set; }
        public string FromStationName { get; set; }
        public string FromStationCode { get; set; }
        public string ToStationName { get; set; }
        public string ToStationCode { get; set; }
    }
}
