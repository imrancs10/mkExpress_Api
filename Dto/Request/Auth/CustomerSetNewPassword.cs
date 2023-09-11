using System.ComponentModel.DataAnnotations;

namespace MKExpress.API.Dto.Request
{
    public class CustomerSetNewPassword
    {
        [Required] public string Token { get; set; }
        [Required] public string Password { get; set; }
    }
}