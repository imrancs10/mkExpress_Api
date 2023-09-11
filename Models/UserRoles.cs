
using MKExpress.API.Models.BaseModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class UserRole : BaseModel
    {
        [Key]
        [Column("RoleId")]
        public override int Id { get => base.Id; set => base.Id = value; }
        public string Name { get; set; }
        public string Code { get; set; }
        public List<UserPermission> UserPermissions { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
