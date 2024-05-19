using MKExpress.API.DTO.Base;

namespace MKExpress.API.DTO.Request
{
    public class AppSettingRequest:BaseRequest
    {
        public string? Key { get; set; }
        public string? Value { get; set; }
        public int GroupId { get; set; }
        public string? DataType { get; set; }
    }
}
