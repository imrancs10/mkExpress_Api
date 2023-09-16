using MKExpress.API.Models.BaseModels;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{

    public class CustomerAccountStatement : BaseModel
    {
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public int? OrderDetailId { get; set; }
        public decimal? Credit { get; set; }
        public decimal? Debit { get; set; }
        public string Reason { get; set; }
        public string Remark { get; set; }
        public string PaymentMode { get; set; }
        public decimal Balance { get; set; }
        public int DeliveredQty { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsFirstAdvance { get; set; }

        //[ForeignKey("OrderId")]
        //public Order Order { get; set; }

        //[ForeignKey("OrderDetailId")]
        //public OrderDetail OrderDetail { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
    }
}