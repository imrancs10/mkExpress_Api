using MKExpress.API.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Request.Orders
{
    public class PagingRequestByWorkType:PagingRequest
    {
       public string WorkType { get; set; }
        public int SalesmanId { get; set; } = 0;
        public string OrderStatus { get; set; } = "active";
    }
}
