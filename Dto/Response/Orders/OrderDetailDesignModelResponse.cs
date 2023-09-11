using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Response.Orders
{
    public class OrderDetailDesignModelResponse
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string ModelNo { get; set; }
        public string OrderNo { get; set; }
        public string Value { get; set; }

    }
}
