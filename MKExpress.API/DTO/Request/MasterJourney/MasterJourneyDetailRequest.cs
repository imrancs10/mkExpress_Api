using MKExpress.API.DTO.Base;

namespace MKExpress.API.DTO.Request
{
    public class MasterJourneyDetailRequest:BaseRequest
    {
        public Guid MasterJourneyId { get; set; }
        public int SequesceNo { get; set; }
        public Guid SubStationId { get; set; }
    }
}
