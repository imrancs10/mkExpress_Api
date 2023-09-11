using System;
using System.Collections.Generic;

namespace MKExpress.API.Dto.Request
{
    public class PurchaseEntryRequest
    {
        public int PurchaseEntryId { get; set; }
        public int PurchaseNo { get; set; }
        public int SupplierId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public List<PurchaseEntryDetailRequest> PurchaseEntryDetails { get; set; }
    }
}
