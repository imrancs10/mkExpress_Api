namespace MKExpress.API.Dto.Request
{

    public class RefreshTokenRequest
    {
        public string? AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}