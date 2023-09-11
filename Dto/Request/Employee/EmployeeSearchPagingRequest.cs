using MKExpress.API.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Request.Employee
{
    public class EmployeeSearchPagingRequest: SearchPagingRequest
    {
        public string Type { get; set; }
        public string Title { get; set; }
    }
}
