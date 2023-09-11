namespace MKExpress.API.Dto.Response
{
    public class ExceptionResponse
    {
        public int StatusCode { get; set; }
        public ErrorResponse ErrorResponse { get; set; }
    }
}