using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Response.Employee
{
    public class EmployeeSalarySlipResponse
    {
        public string VoucherNo { get; set; }
        public DateTime Date { get; set; }
        public string KandooraNo { get; set; }
        public decimal OrderPrice { get; set; }
        public decimal Qty { get; set; }
        public decimal? Amount { get; set; }
        public string Note { get; set; }
        public decimal Extra { get; set; }
        public string EmployeeName { get; set; }
        public string JobTitle { get; set; }
        public string ContactNo { get; set; }
        public int EmployeeId { get; set; }

    }
}
