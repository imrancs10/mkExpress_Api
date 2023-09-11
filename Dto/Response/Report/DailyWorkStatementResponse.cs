using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Response.Report
{
    public class DailyWorkStatementResponse
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string OrderNo { get; set; }
        public DateTime Date { get; set; }
        public string ModalNo { get; set; }
        public string Note { get; set; }
        public decimal Amount { get; set; }
    }
}
