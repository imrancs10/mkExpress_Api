using MKExpress.API.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class Member:BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string Mobile { get; set; }
        public string? IdNumber { get; set; }
        public GenderEnum Gender { get; set; }
        public Guid RoleId { get; set; }
        public Guid? StationId { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("RoleId")]
        public UserRole Role { get; set; }
        [ForeignKey("StationId")]
        public MasterData Station { get; set; }
    }
}
