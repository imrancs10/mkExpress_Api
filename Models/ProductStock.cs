using MKExpress.API.Models.BaseModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class ProductStock : BaseModel
    {
        public int ProductId { get; set; }
        public int AvailableQty { get; set; }
        public int AvailablePiece { get; set; }
        public int UsedQty { get; set; }
        public int UsedPiece { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
