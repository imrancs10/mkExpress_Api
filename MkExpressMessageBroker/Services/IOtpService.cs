using MkExpress.MessageBroker.Enums;

namespace MkExpress.MessageBroker.Services
{
    public interface IOtpService
    {
        string GenerateOtpAsync(string key, OtpLengthEnum otpLength);
        Task<bool> SendOtpAsync(string phoneNumberOrEmail, string otp);
        Task<bool> ValidateOtpAsync(string key, string otp);
    }
}
