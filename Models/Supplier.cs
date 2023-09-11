using MKExpress.API.Models.BaseModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class Supplier : BaseModel
    {
        [Column("SupplierId")]
        [Key]
        public override int Id { get => base.Id; set => base.Id = value; }
        public string CompanyName { get; set; }
        public string Contact { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string TRN { get; set; }
        public string City { get; set; }
        public List<PurchaseEntry> PurchaseEntries { get; set; }
        public List<CrystalPurchase> CrystalPurchases { get; set; }
    }
}
