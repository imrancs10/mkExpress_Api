using System.ComponentModel.DataAnnotations;

namespace MKExpress.API.Models
{
    public class BaseModel
    {
        [Key]
        public virtual Guid Id { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string DeleteNote { get; set; }
    }
}
