using System;

namespace MKExpress.API.Dto.Response
{
    public class CustomerAdvancePaymentResponse
    {
        public int Id { get; set; }
        public decimal Credit { get; set; }
        public string Reason { get; set; }
        public string Remark { get; set; }
        public string PaymentMode { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Balance { get; set; }
        public int DeliveredQty { get; set; }
        public bool IsFirstAdvance { get; set; }
    }
}
