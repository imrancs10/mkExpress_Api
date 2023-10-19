using MKExpress.API.DTO.Base;

namespace MKExpress.API.DTO.Response
{
    public class ThirdPartyCourierResponse:BaseResponse
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string TrackingUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
