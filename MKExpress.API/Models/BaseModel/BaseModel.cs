using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class BaseModel
    {
        [Key]
        public virtual Guid Id { get; set; }
        public Guid? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string? DeleteNote { get; set; }

        [ForeignKey("CreatedBy")]
        public User CreatedByUser { get; set; }
    }
}
