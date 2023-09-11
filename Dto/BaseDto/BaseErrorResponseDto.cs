using System.Text.Json.Serialization;

namespace MKExpress.API.Dto.BaseDto
{

    public class BaseErrorResponseDto
    {
        public string ErrorResponseType { get; set; }
        public string Message { get; set; }

        [JsonIgnore]
        public object Errors { get; set; }
    }
}