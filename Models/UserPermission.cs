using MKExpress.API.Models.BaseModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class UserPermission : BaseModel
    {
        [ForeignKey("RoleId")]
        public int UserRoleId { get; set; }

        [ForeignKey("PermissionResourceId")]
        public int PermissionResourceId { get; set; }

        public PermissionResource PermissionResource { get; set; }
        public UserRole UserRole { get; set; }
    }
}
