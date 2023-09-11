using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Response
{
    public class OrderCrystalResponse
    {
        public int ProductStockId { get; set; }
        public string Product { get; set; }
        public int ProductId { get; set; }
        public string Size { get; set; }
        public string Shape { get; set; }
        public int AvailableQty { get; set; }
        public int AvailablePiece { get; set; }

    }
}
