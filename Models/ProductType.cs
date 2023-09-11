using MKExpress.API.Models.BaseModels;
using System.Collections.Generic;

namespace MKExpress.API.Models
{
    public class ProductType : MasterBaseModel
    {
        public List<Product> Products { get; set; }
    }
}
