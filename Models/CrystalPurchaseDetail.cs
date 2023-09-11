using MKExpress.API.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Models
{
    public class CrystalPurchaseDetail : BaseModel
    { 
        public int CrystalPurchaseId { get; set; }
        public int CrystalId { get; set; }
        public int Qty { get; set; }
        public int PiecePerPacket { get; set; }
        public int TotalPiece { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotalAmount { get; set; }
        public decimal Vat { get; set; }
        public decimal VatAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsCancelled { get; set; }

        [ForeignKey("CrystalPurchaseId")]
        public CrystalPurchase CrystalPurchase { get; set; }
        [ForeignKey("CrystalId")]
        public MasterCrystal MasterCrystal { get; set; }
    }
}
