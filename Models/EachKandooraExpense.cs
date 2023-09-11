using MKExpress.API.Models.BaseModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class EachKandooraExpense : BaseModel
    {
        public int KandooraHeadId { get; set; }
        public decimal Amount { get; set; }

        [ForeignKey("KandooraHeadId")]
        public EachKandooraExpenseHead EachKandooraExpenseHead { get; set; }
    }
}
