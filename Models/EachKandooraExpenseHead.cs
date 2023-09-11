using MKExpress.API.Models.BaseModels;
using System.Collections.Generic;

namespace MKExpress.API.Models
{
    public class EachKandooraExpenseHead : BaseModel
    {
        public string HeadName { get; set; }
        public int DisplayOrder { get; set; }
        public List<EachKandooraExpense> EachKandooraExpenses { get; set; }
    }
}
