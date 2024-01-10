using MKExpress.API.Contants;
using System.ComponentModel.DataAnnotations;

namespace MKExpress.API.Dto.BaseDto
{
    public class BaseUserRegistrationRequest
    {
        [Required]
        [EmailAddress(ErrorMessage = StaticValues.InvalidEmail)]
        public virtual string Email { get; set; }

        [Required] public virtual string Password { get; set; }
        [Required] public virtual string FirstName { get; set; }
        [Required] public virtual string LastName { get; set; }
        public virtual string Mobile { get; set; }
        public virtual string Role { get; set; }
    }
}