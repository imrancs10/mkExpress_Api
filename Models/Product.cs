using MKExpress.API.Models.BaseModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class Product : BaseModel
    {
        public int ProductTypeId { get; set; }
        public string ProductName { get; set; }
        public int? WidthId { get; set; }
        public int? SizeId { get; set; }

        [ForeignKey("WidthId")]
        public MasterData FabricWidth { get; set; }

        [ForeignKey("SizeId")]
        public MasterData Size { get; set; }
        [ForeignKey("ProductTypeId")]
        public ProductType ProductType { get; set; }
        public ProductStock ProductStock { get; set; }
        public List<OrderCrystalDetails> OrderCrystalDetails { get; set; }

    }
}
