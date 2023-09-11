using System;

namespace MKExpress.API.Dto.Request
{
    public class CustomerAdvancePaymentRequest
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal Credit { get; set; }
        public string PaymentMode { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Reason { get; set; }
        public string Remark { get; set; }
        public int CustomerId { get; set; }
    }
}
