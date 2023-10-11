using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class ContainerJourney:BaseModel
    {
        public Guid ContainerId { get; set; }
        public Guid StationId { get; set; }
        public DateTime? ArrivalAt{ get; set; }
        public DateTime? DepartureOn { get; set; }
        public int SequenceNo { get; set; }
        public bool IsSourceStation { get; set; }
        public bool IsDestinationStation { get; set; }

        [ForeignKey("ContainerId")]
        public Container? Container { get; set; }

        [ForeignKey("StationId")]
        public MasterData? Station { get; set; }
    }
}
