namespace MKExpress.API.Dto.Response
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductName { get; set; }
        public int WidthId { get; set; }
        public int SizeId { get; set; }
        public string Width { get; set; }
        public string Size { get; set; }
        public string ProductType { get; set; }
    }
}
