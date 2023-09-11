using System.ComponentModel.DataAnnotations;

namespace MKExpress.API.Dto.Request
{
    public class CustomerResetPasswordRequest
    {
        [Required] [EmailAddress] public string Email { get; set; }
    }
}