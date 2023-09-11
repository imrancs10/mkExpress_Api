using Microsoft.AspNetCore.Http;
using MKExpress.API.Annotations;
using System.ComponentModel.DataAnnotations;

namespace MKExpress.API.Dto.Request.Files
{
    public class DesignSamplePhotoRequest
    {
        [Required]
        [MaxFileSize(5 * 1024 * 1024)] //5MB 
        [AllowedExtensions(new[] { ".png", "image/png", ".jpeg", "image/jpeg", ".jpg" })]
        public IFormFile ProfilePhoto { get; set; }
    }
}