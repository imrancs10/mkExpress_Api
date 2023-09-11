using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Request
{
    public class OrderUsedCrystalRequest
    {
        public int OrderDetailId { get; set; }
        public int EmployeeId { get; set; }
        public int ProductStockId { get; set; }
        public int UsedQty { get; set; }
    }

   
}
