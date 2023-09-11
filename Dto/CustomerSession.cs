using MKExpress.API.Constants;
using System;

namespace MKExpress.API.Dto
{
    public class CustomerSession
    {
        public bool EmailVerified { get; set; }
        public DateTime? OtpGenerationDateTime { get; set; }
        public string? Otp { get; set; }
        public string Email { get; set; }
        public bool OtpVerified { get; set; }
        public EmailTemplateEnum Type { get; set; }
    }
}