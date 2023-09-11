using MKExpress.API.Models.BaseModels;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class CrystalTrackingOutDetail:BaseModel
    {
        public int TrackingOutId { get; set; }
        public int CrystalId { get; set; }
        public decimal ReleasePacketQty { get; set; }
        public int ReleasePieceQty { get; set; }
        public int LoosePieces { get; set; }
        public decimal ArticalLabourCharge { get; set; }
        public decimal CrystalLabourCharge { get; set; }
        public bool IsAlterWork { get; set; }
        public DateTime ReleaseDate { get; set; }

        [ForeignKey("CrystalId")]
        public MasterCrystal Crystal { get; set; }

        [ForeignKey("TrackingOutId")]
        public CrystalTrackingOut CrystalTrackingOut { get; set; }
    }
}
