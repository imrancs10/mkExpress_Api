using MKExpress.API.Models.BaseModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class MasterJobTitle : MasterBaseModel
    {
        [Column("JobTitleId")]
        [Key]
        public override int Id { get => base.Id; set => base.Id = value; }
        public List<Employee> Employees { get; set; }
    }
}
