using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Request
{
    public class CrystalPurchaseRequest
    {
        public int Id { get; set; }
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
        public DateTime InstallmentStartDate { get; set; }
        public int Installments { get; set; }
        public bool IsWithOutVat { get; set; }
        public List<CrystalPurchaseDetailRequest> CrystalPurchaseDetails { get; set; }
    }

    public class CrystalPurchaseDetailRequest
    {
        public int Id { get; set; }
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
    }
}
