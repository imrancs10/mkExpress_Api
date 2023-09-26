using MKExpress.API.DTO.Base;
using MKExpress.API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.DTO.Response
{
    public class LogisticRegionResponse : BaseResponse
    {
        public Guid CountryId { get; set; }
        public Guid ProvinceId { get; set; }
        public Guid CityId { get; set; }
        public Guid? DistrictId { get; set; } = new Guid();
        public Guid StationId { get; set; }
        public Guid ParentStationId { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string? District { get; set; }
        public string Station { get; set; }
        public string ParentStation { get; set; }
    }
}
