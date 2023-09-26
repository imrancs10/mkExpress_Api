using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class LogisticRegion:BaseModel
    {
        public Guid CountryId { get; set; }
        public Guid ProvinceId { get; set; }
        public Guid CityId { get; set; }
        public Guid? DistrictId { get; set; } = null;
        public Guid StationId { get; set; }
        public Guid ParentStationId { get; set; }

        [ForeignKey("CountryId")]
        public MasterData Country { get; set; }
        [ForeignKey("ProvinceId")]
        public MasterData Province { get; set; }
        [ForeignKey("CityId")]
        public MasterData City { get; set; }
        [ForeignKey("DistrictId")]
        public MasterData District { get; set; }
        [ForeignKey("StationId")]
        public MasterData Station { get; set; }
        [ForeignKey("ParentStationId")]
        public MasterData ParentStation { get; set; }

    }
}
