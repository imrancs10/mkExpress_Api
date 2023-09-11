using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Response.Dashboard
{
    public class CrystalDeashboardResponse
    {
        public int BuyQty { get; set; }
        public int BuyPiece { get; set; }
        public decimal UsedQty { get; set; }
        public int UsedPiece { get; set; }
        public decimal BalanceQty { get; set; }
        public int BalancePiece { get; set; }
        public int LowCrystalStockCount { get; set; }
        public int TodayBuyQty { get; set; }
        public decimal TodayBuyPiece { get; set; }
        public decimal TodayUsedQty { get; set; }
        public int TodayUsedPiece { get; set; }
    }
}
