using System;

namespace MKExpress.API.Dto.Request
{
    public class WorkTypeStatusRequest
    {
        public int Id { get; set; }
        public int OrderDetailId { get; set; }
        public int WorkTypeId { get; set; }
        public DateTime CompletedOn { get; set; }
        public decimal? Price { get; set; }
        public int? CompletedBy { get; set; }
        public string Note { get; set; }
        public int Extra { get; set; }
        public bool IsSaved { get; set; }
        public string VoucherNo { get; set; }
    }
}
