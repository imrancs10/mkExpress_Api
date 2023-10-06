using MKExpress.API.DTO.Base;

namespace MKExpress.API.DTO.Response
{
    public class MasterJourneyDetailResponse:BaseResponse
    {
        public Guid MasterJourneyId { get; set; }
        public int SequesceNo { get; set; }
        public Guid SubStationId { get; set; }
        public string SubStationName { get; set; }
        public string SubStationCode { get; set; }
    }
}
