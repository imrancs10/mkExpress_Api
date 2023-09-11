using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Response
{
    public class CrystalTrackingOutDetailResponse
    {
        public int Id { get; set; }
        public string CrystalName { get; set; }
        public int TrackingOutId { get; set; }
        public int CrystalId { get; set; }
        public decimal ReleasePacketQty { get; set; }
        public decimal AlterPackets { get; set; }
        public int ReleasePieceQty { get; set; }
        public int LoosePieces { get; set; }
        public decimal ArticalLabourCharge { get; set; }
        public decimal CrystalLabourCharge { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool IsAlterWork { get; set; }
        public string CrystalBrand { get; set; }
        public string CrystalShape { get; set; }
        public string CrystalSize { get; set; }
    }
    public class CrystalConsumeDetailResponse
    {
        public string CrystalName { get; set; }
        public int CrystalId { get; set; }
        public decimal ReleasePacketQty { get; set; }
        public decimal AlterPackets { get; set; }
        public int ReleasePieceQty { get; set; }
        public int LoosePieces { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int TotalOrders { get; set; }
         public string CrystalBrand { get; set; }
        public string CrystalShape { get; set; }
        public string CrystalSize  { get; set; }
}
}

