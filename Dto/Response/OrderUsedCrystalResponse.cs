using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Response
{
    public class OrderUsedCrystalResponse
    {
        public int Id { get; set; }
        public int OrderDetailId { get; set; }
        public int EmployeeId { get; set; }
        public int ProductStockId { get; set; }
        public int UsedQty { get; set; }
        public string Product { get; set; }
        public int ProductId { get; set; }
        public string Size { get; set; }
        public string Shape { get; set; }
    }
}
