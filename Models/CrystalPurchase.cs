using MKExpress.API.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Models
{
    public class CrystalPurchase:BaseModel
    {
        public int PurchaseNo { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int SupplierId { get; set; }
        public bool IsCancelled { get; set; }
        public int Qty { get; set; }
        public decimal SubTotalAmount { get; set; }
        public decimal Vat { get; set; }
        public decimal VatAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMode { get; set; }
        public string ChequeNo { get; set; }
        public DateTime ChequeDate { get; set; }
        public int Installments { get; set; }
        public DateTime InstallmentStartDate { get; set; }
        public bool IsWithOutVat { get; set; }
        [ForeignKey("SupplierId")]
        public Supplier Supplier { get; set; }
        public List<CrystalPurchaseDetail> CrystalPurchaseDetails { get; set; }
        public List<CrystalPurchaseInstallment> CrystalPurchaseInstallments { get; set; }

    }
}
