using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class MasterJourney:BaseModel
    {
        public Guid FromStationId { get; set; }
        public Guid ToStationId { get; set; }

        [ForeignKey("FromStationId")]
        public MasterData? FromStation { get; set; }
        [ForeignKey("ToStationId")]
        public MasterData? ToStation { get; set; }

        public List<MasterJourneyDetail> MasterJourneyDetails { get; set; }
    }
}
