using System.ComponentModel.DataAnnotations;

namespace MKExpress.API.DTO
{
    public class BaseImageUploadDTO
    {
        [Required]
        public IFormFile? File { get; set; }
    }
}
