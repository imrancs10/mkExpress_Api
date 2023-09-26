using MKExpress.API.DTO.Base;

namespace MKExpress.API.DTO.Request
{
    public class LogisticRegionRequest:BaseRequest
    {
        public Guid CountryId { get; set; }
        public Guid ProvinceId { get; set; }
        public Guid CityId { get; set; }
        public Guid? DistrictId { get; set; }=new Guid();
        public Guid StationId { get; set; }
        public Guid ParentStationId { get; set; }
    }
}
