using MKExpress.API.Dto.Response.Customer;
using MKExpress.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Response.Report
{
    public class DailyStatusResponse
    {
        public List<OrderResponse> Orders { get; set; }
        public List<CustomerAccountStatementResponse> CustomerAccountStatements { get; set; }
        public decimal ExpenseAmount { get; set; }
    }
}
