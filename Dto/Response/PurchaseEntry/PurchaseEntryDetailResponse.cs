using System;

namespace MKExpress.API.Dto.Response
{
    public class PurchaseEntryDetailResponse
    {
        public int PurchaseEntryDetailId { get; set; }
        public int PurchaseEntryId { get; set; }
        public int FabricWidthId { get; set; }
        public int ProductId { get; set; }
        public decimal Qty { get; set; }
        public string Barcode { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal VatAmount { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Description { get; set; }
        public string FabricWidth { get; set; }
        public string ProductName { get; set; }
    }
}
