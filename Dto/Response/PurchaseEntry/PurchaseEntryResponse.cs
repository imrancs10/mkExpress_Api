using System;
using System.Collections.Generic;

namespace MKExpress.API.Dto.Response
{
    public class PurchaseEntryResponse
    {
        public int PurchaseEntryId { get; set; }
        public int PurchaseNo { get; set; }
        public int SupplierId { get; set; }
        public string Supplier { get; set; }
        public string CompanyName { get; set; }
        public string TRN { get; set; }
        public string ContactNo { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string CreatedBy { get; set; }
        public List<PurchaseEntryDetailResponse> PurchaseEntryDetails { get; set; }
    }
}
