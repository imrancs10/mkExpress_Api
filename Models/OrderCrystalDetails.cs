using MKExpress.API.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Models
{
    public class OrderCrystalDetails:BaseModel
    {
        public int OrderDetailId { get; set; }
        public int ProductStockId { get; set; }
        public int UsedPiece { get; set; }

        [ForeignKey("ProductStockId")]
        public ProductStock ProductStock { get; set; }
    }
}
