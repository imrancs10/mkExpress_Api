using MKExpress.API.Models.BaseModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{

    public class Customer : BaseModel
    {
        [Column("CustomerId")]
        [Key]
        public override int Id { get => base.Id; set => base.Id = value; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        public string Branch { get; set; }
        public string POBox { get; set; }
        public string TRN { get; set; }
        //public List<Order> Orders { get; set; }
        public List<CustomerAccountStatement> AccountStatements { get; set; }
    }
}
