using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Request.Orders
{
    public class OrderEditRequest
    {
        public int OrderId { get; set; }
        public string PaymentMode { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int CustomerId { get; set; }
    }
}
