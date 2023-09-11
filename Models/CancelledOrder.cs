using MKExpress.API.Models.BaseModels;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class CancelledOrder : BaseModel
    {

        public int OrderId { get; set; }

        public decimal? OrderQty { get; set; }

        public string CustomerName { get; set; }

        public decimal? DueAmount { get; set; }

        public decimal? PaidAmount { get; set; }

        public decimal? OrderBalance { get; set; }

        public DateTime? OrderDate { get; set; }

        public string OrderNote { get; set; }

        public decimal? OrderVat { get; set; }

        public DateTime? OrderBDate { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }

}
