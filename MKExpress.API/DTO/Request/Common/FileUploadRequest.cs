using MKExpress.API.Contants;
using MKExpress.API.Enums;
using Newtonsoft.Json;

namespace MKExpress.API.DTO.Request
{
    public class FileUploadQueryRequest
    {
        public ModuleNameEnum ModuleName { get; set; }
        public bool CreateThumbnail { get; set; } = true;
        public string Remark { get; set; }
        public int SequenceNo { get; set; }
        public string FileType { get; set; } = FileTypeEnum.Image.ToString();
    }
    public class FileUploadRequest: FileUploadQueryRequest
    {
        //[AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg", "image/png", "image/jpeg" })]
        //[MaxFileSize(StaticValues.MaxAllowedFileSize)]
        [JsonIgnore]
        public IFormFile File { get; set; }
        public  Guid ShipmentId { get; set; } 
        public  Guid? TrackingId { get; set; }
    }
}
