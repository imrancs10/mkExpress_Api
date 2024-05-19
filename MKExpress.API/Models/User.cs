using MKExpress.API.Enums;

namespace MKExpress.API.Models
{
    public class User: BaseModel
    {

        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public GenderEnum Gender { get; set; }
        public string Mobile { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsLocked { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsTcAccepted { get; set; }
        public bool IsResetCodeInitiated { get; set; }
        public string? PasswordResetCode { get; set; }
        public DateTime? PasswordResetCodeExpireOn { get; set; }
        public string? EmailVerificationCode { get; set; }
        public string Role { get; set; } = "User";
        public bool IsCustomer { get; set; }
        public DateTime? EmailVerificationCodeExpireOn { get; set; }
    }
}
