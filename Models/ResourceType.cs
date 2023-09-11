using MKExpress.API.Models.BaseModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class ResourceType : BaseModel
    {
        [Key]
        [Column("ResourceTypeId")]
        public override int Id { get => base.Id; set => base.Id = value; }
        public string Name { get; set; }

        public List<PermissionResource> PermissionResources { get; set; }
    }
}
