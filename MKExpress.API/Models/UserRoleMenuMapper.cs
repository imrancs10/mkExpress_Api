using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class UserRoleMenuMapper:BaseModel
    {
        public Guid RoleId { get; set; }
        public Guid MenuId { get; set; }

        [ForeignKey("RoleId")]
        public UserRole? UserRole { get; set; }
        [ForeignKey("MenuId")]
        public Menu? Menu { get; set; }
    }
}
