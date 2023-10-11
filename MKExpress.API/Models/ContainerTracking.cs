using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class ContainerTracking:BaseModel
    {
        public Guid ContainerJourneyId { get; set; }
        public Guid ContainerId { get; set; }
        public string Code { get; set; }
        public Guid CreatedById { get; set; }

        [ForeignKey("ContainerId")]
        public Container Container { get; set; }

        [ForeignKey("CreatedById")]
        public Member CreatedMember { get; set; }

        [ForeignKey("ContainerJourneyId")]
        public ContainerJourney ContainerJourney { get; set; }
    }
}
