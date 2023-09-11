using MKExpress.API.Models.BaseModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class OrderUsedCrystal : BaseModel
    {
        public int OrderDetailId { get; set; }
        public int EmployeeId { get; set; }
        public int ProductStockId { get; set; }
        public int UsedQty { get; set; }

        [ForeignKey("OrderDetailId")]
        public OrderDetail OrderDetail { get; set; }

        [ForeignKey("ProductStockId")]
        public ProductStock ProductStock { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }
}
