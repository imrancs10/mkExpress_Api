using System.ComponentModel.DataAnnotations;

namespace MKExpress.API.Dto.Request
{
    public class LoginRequest
    {
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
    }
}