using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class MasterJourneyDetail:BaseModel
    {
        public Guid MasterJourneyId { get; set; }
        [ForeignKey("MasterJourneyId")]
        public MasterJourney MasterJouney { get; set; }
        public Guid SubStationId { get; set; }
        [ForeignKey("SubStationId")]
        public MasterData SubStation { get; set; }
        public int SequenceNo { get; set; }
    }
}
