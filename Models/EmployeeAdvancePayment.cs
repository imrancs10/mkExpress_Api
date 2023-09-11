using MKExpress.API.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class EmployeeAdvancePayment : BaseModel
    {
        public int EmployeeId { get; set; }
        public decimal Amount { get; set; }
        public int EMI { get; set; }
        public int EMIStartMonth { get; set; }
        public int EMIStartYear { get; set; }
        public string Reason { get; set; }
        public DateTime AdvanceDate { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public List<EmployeeEMIPayment> EmployeeEMIPayments { get; set; }
    }

    public class EmployeeEMIPayment : BaseModel
    {
        public int AdvancePaymentId { get; set; }
        public decimal Amount { get; set; }
        public int DeductionMonth { get; set; }
        public int DeductionYear { get; set; }
        public string Remark { get; set; }

        [ForeignKey("AdvancePaymentId")]
        public EmployeeAdvancePayment EmployeeAdvancePayment { get; set; }
    }
}
