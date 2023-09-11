
using System;
using System.Collections.Generic;

namespace MKExpress.API.Dto.Request
{
    public class OrderRequest
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderDeliveryDate { get; set; }
        public string City { get; set; }
        public decimal SubTotalAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal VAT { get; set; }
        public string PaymentMode { get; set; }
        public string CustomerRefName { get; set; }
        public string Note { get; set; }
        public int? Qty { get; set; }
        public List<OrderDetailRequest> OrderDetails { get; set; }
    }
}
