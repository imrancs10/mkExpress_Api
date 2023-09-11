using System;

namespace MKExpress.API.Dto.Response
{
    public class WorkTypeStatusResponse
    {
        public int Id { get; set; }
        public DateTime CompletedOn { get; set; }
        public decimal? Price { get; set; }
        public int? CompletedBy { get; set; }
        public string CompletedByName { get; set; }
        public string CreatedBy { get; set; }
        public string WorkType { get; set; }
        public int OrderDetailId { get; set; }
        public DateTime DeliveredDate { get; set; }
        public int WorkTypeId { get; set; }
        public string OrderNo { get; set; }
        public string KandooraNo { get; set; }
        public string Note { get; set; }
        public string AddtionalNote { get; set; }
        public bool IsSaved { get; set; }
        public decimal Extra { get; set; }
        public int WorkTypeSequence { get; set; }
        public string VoucherNo { get; set; }
    }
}
