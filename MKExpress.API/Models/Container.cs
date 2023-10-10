using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class Container:BaseModel
    {
        public int ContainerNo { get; set; }
        public Guid ContainerTypeId { get; set; }
        public DateTime? ClosedOn { get; set; }
        public Guid? ClosedBy { get; set; }
        public Guid JourneyId { get; set; }

        public List<ContainerDetail> ContainerDetails { get; set; }
        public List<ContainerJourney> ContainerJourneys { get; set; }

        [ForeignKey("ClosedBy")]
        public Member ClosedByMember { get; set; }

        [ForeignKey("JourneyId")]
        public MasterJourney Journey { get; set; }

        [ForeignKey("ContainerTypeId")]
        public MasterData ContainerType { get; set; }
    }
}
