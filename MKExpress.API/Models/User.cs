using MKExpress.API.Enums;
using System.ComponentModel.DataAnnotations.Schema;

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
        public Guid? RoleId { get; set; }
        public Guid? MemberId { get; set; }
        public bool IsCustomer { get; set; }
        public DateTime? EmailVerificationCodeExpireOn { get; set; }
        [ForeignKey("RoleId")]
        public UserRole UserRole { get; set; }
        public string? ProfileImagePath { get; set; }
    }
}
