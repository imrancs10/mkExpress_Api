using MKExpress.API.DTO.BaseDto;

namespace MKExpress.API.DTO.Request
{
    public class SystemActionRequest:BasePagingRequest
    {
        public string? ActionType { get; set; }
        public DateTime? ActionFrom { get; set; }
        public DateTime? ActionTo { get; set; }
        public Guid? StationId { get; set; }
        public Guid? ConsigneeCityId { get; set; }
    }
}
