using MKExpress.API.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class PurchaseEntry : BaseModel
    {
        [Key]
        [Column("PurchaseEntryId")]
        public override int Id { get => base.Id; set => base.Id = value; }
        public int PurchaseNo { get; set; }
        public int SupplierId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier Supplier { get; set; }

        public List<PurchaseEntryDetail> PurchaseEntryDetails { get; set; }

        [ForeignKey("CreatedBy")]
        public Employee EmployeeCreatedBy { get; set; }
    }
}
