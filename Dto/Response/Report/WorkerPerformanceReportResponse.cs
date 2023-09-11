using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Response.Report
{
    public class WorkerPerformanceReportResponse
    {
        public int WorkerId { get; set; }
        public string WorkerName { get; set; }
        public decimal Amount { get; set; }
        public decimal AvgAmount { get; set; }
        public int Qty { get; set; }
    }
}
