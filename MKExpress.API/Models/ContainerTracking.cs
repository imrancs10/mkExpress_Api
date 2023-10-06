using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class ContainerTracking:BaseModel
    {
        public Guid ContainerId { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid StationId { get; set; }
        public string Code { get; set; }

        [ForeignKey("StationId")]
        public MasterData Station { get; set; }

        [ForeignKey("CreatedBy")]
        public Member CreatedByMember { get; set; }

        [ForeignKey("ContainerId")]
        public Container Container { get; set; }
    }
}
