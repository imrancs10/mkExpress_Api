using MKExpress.API.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Models
{
    public class CrystalStock:BaseModel
    {
        public int CrystalId { get; set; }
        public int InStock { get; set; }
        public int InStockPieces { get; set; }
        public int OutStock { get; set; }
        public int OutStockPieces { get; set; }
        public int BalanceStock { get; set; }
        public int BalanceStockPieces { get; set; }
        [ForeignKey("CrystalId")]
        public MasterCrystal MasterCrystal { get; set; }
    }
}
