using DocumentFormat.OpenXml.Bibliography;
using MKExpress.API.DTO.Response;

namespace MKExpress.API.DTO.Response
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public bool IsAuthenticated { get; set; } = false;
        public UserResponse UserResponse { get; set; }
    }
}
