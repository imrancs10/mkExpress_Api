using MKExpress.API.Models.BaseModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class PermissionResource : BaseModel
    {
        [Key]
        [Column("PermissionResourceId")]
        public override int Id { get => base.Id; set => base.Id = value; }

        [ForeignKey("ResourceTypeId")]
        public int ResourceTypeId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public ResourceType ResourceType { get; set; }
        public List<UserPermission> UserPermissions { get; set; }
    }
}
