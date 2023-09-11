using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Request
{
    public class CrystalTrackingOutDetailRequest
    {
        public int Id { get; set; }
        public int TrackingOutId { get; set; }
        public int CrystalId { get; set; }
        public decimal ReleasePacketQty { get; set; }
        public int ReleasePieceQty { get; set; }
        public int LoosePieces { get; set; }
        public decimal ArticalLabourCharge { get; set; }
        public decimal CrystalLabourCharge { get; set; }
        public bool IsAlterWork { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
