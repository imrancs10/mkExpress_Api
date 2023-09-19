namespace MKExpress.API.DTO.Base
{
    public class BaseErrorResponse
    {
            public string ErrorResponseType { get; set; }
            public string Message { get; set; }
            public object Errors { get; set; }
    }
}
