using MKExpress.API.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Request
{
    public class DailyWorkStatementPagingRequest : PagingRequest
    {
        public int ReportType { get; set; }
        public int WorkType { get; set; }
        public string SearchTerm { get; set; }
    }
}
