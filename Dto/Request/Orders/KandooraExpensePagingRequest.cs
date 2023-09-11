using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Request
{
    public class KandooraExpensePagingRequest : PagingRequest
    {
        public int SalesmanId { get; set; }
        public string OrderStatus { get; set; }
        public int ProfitPercentageFilter { get; set; }
    }
}
