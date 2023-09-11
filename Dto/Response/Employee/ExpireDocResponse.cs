using System;

namespace MKExpress.API.Dto.Response.Employee
{
    public class ExpireDocResponse
    {
        public string DocumentName { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime? ExpireAt { get; set; }
        public string Status { get; set; }

    }
}
