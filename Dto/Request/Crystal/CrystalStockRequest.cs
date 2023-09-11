using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Request
{
    public class CrystalStockRequest
    {
        public int Id { get; set; }
        public int CrystalId { get; set; }
        public int InStock { get; set; }
        public int InStockPieces { get; set; }
        public int BalanceStock { get; set; }
        public int BalanceStockPieces { get; set; }
    }
}
