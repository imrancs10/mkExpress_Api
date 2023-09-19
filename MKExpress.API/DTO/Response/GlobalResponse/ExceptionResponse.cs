using MKExpress.API.DTO.Response;

namespace SalehGarib.API.DTO.Response
{
    public class ExceptionResponse
    {
        public int StatusCode { get; set; }
        public ErrorResponse ErrorResponse { get; set; }
    }
}