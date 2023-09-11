using System;

namespace MKExpress.API.Dto.Request
{
    public class PurchaseEntryDetailRequest
    {
        public int PurchaseEntryDetailId { get; set; }
        public int PurchaseEntryId { get; set; }
        public int FabricWidthId { get; set; }
        public int ProductId { get; set; }
        public decimal Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal VatAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Description { get; set; }
    }
}
