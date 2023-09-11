using System;

namespace MKExpress.API.Dto.Request.Orders
{
    public class OrderAlertRequest : PagingRequest
    {
        public int AlertBeforeDays { get; set; } = 0;
        public int SalesmanId { get; set; }
    }
}
