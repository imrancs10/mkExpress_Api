using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Request
{
    public class ExpensePagingRequest:PagingRequest
    {
        public int ExpenseNameId { get; set; }
    }
}
