using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Response
{
    public class HeadWiseExpenseSumResponse
    {
        public string ExpenseName { get; set; }
        public string ExpenseType { get; set; }
        public decimal Amount { get; set; }
    }
}
