using System;
using System.Collections.Generic;

namespace MKExpress.API.Dto.Request
{
    public class DeliveryPaymentRequest
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int OrderDetailId { get; set; }
        public decimal PaidAmount { get; set; }
        public string OrderNo { get; set; }
        public string PaymentMode { get; set; }
        public List<int> DeliveredKandoorIds { get; set; }
        public bool AllDelivery { get; set; }
        public DateTime DeliveredOn { get; set; }
        public int TotalKandoorInOrder { get; set; }
        public decimal OrderBalanceAmount { get; set; }
    }
}
