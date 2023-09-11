using MKExpress.API.Models.BaseModels;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class WorkTypeStatus : BaseModel
    {
        public int OrderDetailId { get; set; }
        public string VoucherNo { get; set; }
        public int WorkTypeId { get; set; }
        public DateTime CompletedOn { get; set; }
        public decimal? Price { get; set; }
        public int? CompletedBy { get; set; }
        public string Note { get; set; }
        public string AddtionalNote { get; set; }
        public bool IsSaved { get; set; } = false;
        public decimal? Extra { get; set; }
        [ForeignKey("OrderDetailId")]
        public OrderDetail OrderDetail { get; set; }

        [ForeignKey("WorkTypeId")]
        public MasterData WorkType { get; set; }

        [ForeignKey("CreatedBy")]
        public Employee CreatedByEmployee { get; set; }

        [ForeignKey("UpdatedBy")]
        public Employee UpdatedByEmployee { get; set; }

        [ForeignKey("CompletedBy")]
        public Employee CompletedByEmployee { get; set; }
    }
}
