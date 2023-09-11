using MKExpress.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Response.Customer
{
    public class CustomerAccountStatementResponse
    {
        public int Id { get; set; }
        public decimal? Credit { get; set; }
        public decimal? Debit { get; set; }
        public string Reason { get; set; }
        public string Remark { get; set; }
        public string PaymentMode { get; set; }
        public DateTime PaymentDate { get; set; }
        public OrderResponse Order { get; set; }
        public decimal Balance{ get; set; }
        public int DeliveredQty { get; set; }
        public bool IsFirstAdvance { get; set; }
    }
}
