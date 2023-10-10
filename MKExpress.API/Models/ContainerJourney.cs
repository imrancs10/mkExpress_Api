using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class ContainerJourney:BaseModel
    {
        public Guid ContainerId { get; set; }
        public string StationName { get; set; }
        public DateTime ArrivalAt{ get; set; }
        public DateTime DepartureOn { get; set; }
        public int SequenceNo { get; set; }
        [ForeignKey("ContainerId")]
        public Container Container { get; set; }
    }
}
