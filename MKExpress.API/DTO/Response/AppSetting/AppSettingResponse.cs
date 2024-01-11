using MKExpress.API.DTO.Base;

namespace MKExpress.API.DTO.Response
{
    public class AppSettingResponse:BaseResponse
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public int Group { get; set; }
    }
}
