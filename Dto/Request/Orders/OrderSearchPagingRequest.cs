using MKExpress.API.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Request.Orders
{
    public class OrderSearchPagingRequest: SearchPagingRequest
    {
        public DateTime FromDate { get; set; } = DateTime.MinValue;
        public DateTime ToDate { get; set; } = DateTime.MinValue;
    }
}
