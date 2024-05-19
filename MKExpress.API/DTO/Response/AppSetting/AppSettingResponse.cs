using MKExpress.API.DTO.Base;

namespace MKExpress.API.DTO.Response
{
    public class AppSettingResponse:BaseResponse
    {
        public string? Key { get; set; }
        public string? Value { get; set; }
        public string? DataType { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
    }
}
