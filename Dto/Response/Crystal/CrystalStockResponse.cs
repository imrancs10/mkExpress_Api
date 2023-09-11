using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Response
{
    public class CrystalStockResponse
    {
        public int Id { get; set; }
        public int CrystalId { get; set; }
        public string CrystalName { get; set; }
        public string CrystalShape { get; set; }
        public string CrystalSize { get; set; }
        public string CrystalBrand { get; set; }
        public int InStock { get; set; }
        public int InStockPieces { get; set; }
        public int OutStock { get; set; }
        public int OutStockPieces { get; set; }
        public int BalanceStock { get; set; }
        public int BalanceStockPieces { get; set; }
        public int AlertQty { get; set; }
        public int QtyPerPacket { get; set; }
    }

    public class CrystalStockResponseExt: CrystalStockResponse
    {
        public int OldStock { get; set; }
        public int NewStock { get; set; }
        public decimal ConsumeStock { get; set; }
        public decimal TotalStock { get; set; }
    }
}
