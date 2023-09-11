
using System;

namespace MKExpress.API.Dto.Request
{
    public class EmployeeAdvancePaymentRequest
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public decimal Amount { get; set; }
        public int EMI { get; set; }
        public int EMIStartMonth { get; set; }
        public int EMIStartYear { get; set; }
        public string Reason { get; set; }
        public DateTime AdvanceDate { get; set; }
    }
}
