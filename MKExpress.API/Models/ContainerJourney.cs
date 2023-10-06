using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class ContainerJourney:BaseModel
    { 
        public Guid FromStationId { get; set; }
        public Guid ToStationId { get; set; }

        [ForeignKey("FromStationId")]
        public MasterData? FromStation { get; set; }
        [ForeignKey("ToStationId")]
        public MasterData? ToStation { get; set; }
        [ForeignKey("ContainerId")]
        public Container Container { get; set; }
    }
}
