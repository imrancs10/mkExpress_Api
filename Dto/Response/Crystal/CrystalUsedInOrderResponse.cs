namespace MKExpress.Web.API.Dto.Response.Crystal
{
    public class CrystalUsedInOrderResponse
    {
        public string OrderNo { get; set; }
        public decimal Packets { get; set; }
        public int Pieces { get; set; }
        public int LoosePieces { get; set; }
        public decimal AlterPackets { get; set; }
    }
}
