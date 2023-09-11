using MKExpress.API.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Models
{
    public class MasterCrystal:BaseModel
    {
        public string Name { get; set; }
        public int CrystalId { get; set; }
        public int AlertQty { get; set; }
        public int BrandId { get; set; }
        public int SizeId { get; set; }
        public int ShapeId { get; set; }
        public int QtyPerPacket { get; set; }
        public string Barcode { get; set; }
        public bool IsArtical { get; set; } = false;

        [ForeignKey("BrandId")]
        public MasterData Brand { get; set; }
        [ForeignKey("SizeId")]
        public MasterData Size { get; set; }
        [ForeignKey("ShapeId")]
        public MasterData Shape { get; set; }
        public List<CrystalStock> CrystalStocks { get; set; }
        public List<CrystalStockUpdateHistory> CrystalStockUpdateHistories { get; set; }
        public List<CrystalPurchaseDetail> crystalPurchaseDetails { get; set; }

    } 
}
