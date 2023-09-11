using System;
using System.Collections.Generic;

namespace MKExpress.API.Dto.Response
{
    public class EmployeeAdvancePaymentResponse
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeResponse Employee { get; set; }
        public decimal Amount { get; set; }
        public int EMI { get; set; }
        public int EMIStartMonth { get; set; }
        public int EMIStartYear { get; set; }
        public string Reason { get; set; }
        public DateTime AdvanceDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<EmployeeEMIPaymentResponse> EmployeeEMIPayments { get; set; }
    }
}
