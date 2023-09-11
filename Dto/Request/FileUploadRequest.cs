using Microsoft.AspNetCore.Http;
using MKExpress.API.Annotations;
using MKExpress.API.Constants;
using MKExpress.API.Enums;

namespace MKExpress.API.Dto.Request
{
    public class FileUploadRequest
    {
        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg", "image/png", "image/jpeg" })]
        [MaxFileSize(StaticValues.MaxAllowedFileSize)]
        public IFormFile File { get; set; }
        public int ModuleId { get; set; }
        public ModuleNameEnum ModuleName { get; set; }
        public bool CreateThumbnail { get; set; } = true;
        public string Remark { get; set; }
    }
}
