using Microsoft.AspNetCore.Http;
using MKExpress.API.Annotations;
using MKExpress.API.Constants;

namespace MKExpress.API.Dto.Request
{
    public class DesignSampleRequest
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string DesignerName { get; set; }
        public string Shape { get; set; }
        public int Size { get; set; }
        public int Quantity { get; set; }

        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg", "image/png", "image/jpeg" })]
        [MaxFileSize(StaticValues.MaxAllowedFileSize)]
        public IFormFile File { get; set; }
        public int CategoryId { get; set; }
    }
}
