using MKExpress.API.Models.BaseModels;

namespace MKExpress.API.Models
{
    public class ExpenseShopCompany : BaseModel
    {
        public string CompanyName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
    }
}
