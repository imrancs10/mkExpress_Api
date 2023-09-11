namespace MKExpress.API.Dto.Response
{
    public class DesignSampleResponse
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string DesignerName { get; set; }
        public string Shape { get; set; }
        public int Size { get; set; }
        public int Quantity { get; set; }
        public string PicturePath { get; set; }
        public string ThumbPath { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
