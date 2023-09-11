using MKExpress.API.Models.BaseModels;
using System;

namespace MKExpress.API.Models
{
    public class ExpenseVoucher : BaseModel
    {
        public decimal VoucherNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Voucherdate { get; set; }
        public decimal? Amount { get; set; }
        public decimal? AdvanceAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? ExpenseID { get; set; }
        public string ExpenseImage { get; set; }
    }
}

