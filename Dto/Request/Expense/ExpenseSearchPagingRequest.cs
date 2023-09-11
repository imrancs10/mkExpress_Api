using MKExpress.API.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Request
{
    public class ExpenseSearchPagingRequest:SearchPagingRequest
    {
        public int ExpenseNameId { get; set; }
    }
}
