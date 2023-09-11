using MKExpress.API.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Models
{
    public class CrystalPurchaseInstallment:BaseModel
    {
        public int CrystalPurchaseId { get; set; }
        public decimal Amount { get; set; }
        public int InstallmentNo { get; set; }
        public DateTime InstallmentDate { get; set; }
        public bool IsPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMode { get; set; }

        [ForeignKey("CrystalPurchaseId")]
        public CrystalPurchase CrystalPurchase { get; set; }
    }
}
