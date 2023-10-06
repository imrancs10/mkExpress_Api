using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class Container:BaseModel
    {
        public int ContainerNo { get; set; }
        public string ContainerType { get; set; }
        public int TotalShipments { get; set; }
        public DateTime? ClosedOn { get; set; }
        public Guid? ClosedBy { get; set; }

        [ForeignKey("ClosedBy")]
        public Member ClosedByMember { get; set; }
    }
}
