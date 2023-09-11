using MKExpress.API.Models.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class PurchaseEntryDetail : BaseModel
    {
        [Key]
        [Column("PurchaseEntryDetailId")]
        public override int Id { get => base.Id; set => base.Id = value; }
        public int PurchaseEntryId { get; set; }
        public int ProductId { get; set; }
        public decimal Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal VatAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Description { get; set; }

        [ForeignKey("PurchaseEntryId")]
        public PurchaseEntry PurchaseEntry { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
