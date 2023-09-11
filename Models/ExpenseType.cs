using MKExpress.API.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class ExpenseType : MasterBaseModel
    {
        public List<ExpenseName> ExpenseNames { get; set; }
    }

    public class ExpenseName : MasterBaseModel
    {

        public int ExpenseTypeId { get; set; }

        [ForeignKey("ExpenseTypeId")]
        public ExpenseType ExpenseType { get; set; }
    }

    public class Expense : BaseModel
    {
        public int ExpenseNo { get; set; }
        public int ExpenseNameId { get; set; }
        public int? CompanyId { get; set; }
        public int? EmpJobTitleId { get; set; }
        public string PaymentMode { get; set; }
        public DateTime ExpenseDate { get; set; }
        public int? EmplopeeId { get; set; }

        [ForeignKey("EmplopeeId")]
        public Employee Employee { get; set; }

        [ForeignKey("ExpenseNameId")]
        public ExpenseName ExpenseName { get; set; }

        [ForeignKey("CompanyId")]
        public ExpenseShopCompany ExpenseShopCompany { get; set; }

        [ForeignKey("EmpJobTitle")]
        public MasterJobTitle JobTitle { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EmployeeName { get; set; }
        public decimal Amount { get; set; }
        public int ReferenceId { get; set; }
        public string ReferenceName { get; set; }

    }
}
