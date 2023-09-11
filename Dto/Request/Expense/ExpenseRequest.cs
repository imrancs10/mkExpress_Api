using System;

namespace MKExpress.API.Dto.Request.Expense
{
    public class ExpenseRequest
    {
        public int Id { get; set; }
        public int ExpenseNo { get; set; }
        public int ExpenseNameId { get; set; }
        public int? CompanyId { get; set; }
        public int EmpJobTitleId { get; set; }
        public string PaymentMode { get; set; }
        public int EmplopeeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EmployeeName { get; set; }
        public decimal Amount { get; set; }
        public DateTime? ExpenseDate { get; set; }
    }
}
