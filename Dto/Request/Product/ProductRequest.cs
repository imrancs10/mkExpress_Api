namespace MKExpress.API.Dto.Request
{
    public class ProductRequest
    {
        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductName { get; set; }
        public int WidthId { get; set; }
        public int SizeId { get; set; }
    }
}
