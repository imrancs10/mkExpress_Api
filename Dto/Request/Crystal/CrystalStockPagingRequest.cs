
namespace MKExpress.API.Dto.Request
{
    public class CrystalStockPagingRequest:PagingRequest
    {
        public int BrandId { get; set; }
        public int ShapeId { get; set; }
        public int SizeId { get; set; }
    }
}
