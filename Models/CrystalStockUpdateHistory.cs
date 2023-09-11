using MKExpress.API.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Models
{
    public class CrystalStockUpdateHistory:BaseModel
    {
        public int CrystalId { get; set; }
        public int OldPieces { get; set; }
        public int OldPackets { get; set; }
        public int NewPieces { get; set; }
        public int NewPackets { get; set; }
        public string Reason { get; set; }
        public string Remark { get; set; }

        [ForeignKey("CrystalId")]
        public MasterCrystal Crystal { get; set; }
    }
}
