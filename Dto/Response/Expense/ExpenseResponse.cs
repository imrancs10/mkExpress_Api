using System;

namespace MKExpress.API.Dto.Response.Expense
{
    public class ExpenseResponse
    {
        public int Id { get; set; }
        public int ExpenseNo { get; set; }
        public int ExpenseNameId { get; set; }
        public int ExpenseTypeId { get; set; }
        public int CompanyId { get; set; } // NOt in use
        public int EmpJobTitleId { get; set; }
        public int EmplopeeId { get; set; }
        public string ExpenseType { get; set; }
        public string PaymentMode { get; set; }
        public string ExpenseName { get; set; }
        public string ExpenseShopCompany { get; set; }// NOt in use
        public string JobTitle { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EmployeeName { get; set; }
        public string EnteredEmployeeName { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpenseDate { get; set; }
    }
}
