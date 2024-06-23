using Microsoft.Extensions.Caching.Memory;
using MkExpress.MessageBroker.Enums;

namespace MkExpress.MessageBroker.Services
{
    public class OtpService: IOtpService
    {
        private readonly ISmsService _smsService;
        private readonly IEmailService _emailService; 
        private readonly IMemoryCache _otpStorage;

        public OtpService(ISmsService smsService, IEmailService emailService, IMemoryCache otpStorage)
        {
            _smsService = smsService;
            _emailService = emailService; 
            _otpStorage = otpStorage;
        }

        public string GenerateOtpAsync(string key,OtpLengthEnum otpLength=OtpLengthEnum.Digit4)
        {
            var otp = GenerateRandomOtp((int)otpLength);
            _otpStorage.Set(key, otp, TimeSpan.FromMinutes(5));
            return otp;
        }

        public async Task<bool> SendOtpAsync(string phoneNumberOrEmail, string otp)
        {
            try
            {
                if (phoneNumberOrEmail.Contains("@"))
                {
                    await _emailService.SendEmailAsync(phoneNumberOrEmail, "Your OTP", $"Your OTP is {otp}");
                }
                else
                {
                    await _smsService.SendSmsAsync(phoneNumberOrEmail, $"Your OTP is {otp}");
                }
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                return false;
            }
        }

        public async Task<bool> ValidateOtpAsync(string key, string otp)
        {
            if (_otpStorage.TryGetValue(key, out string? storedOtp))
            {
                // Validate and remove the OTP from storage to prevent reuse
                if (storedOtp==null || storedOtp != otp)
                {
                    return false;
                } 
                _otpStorage.Remove(key);
                    return true;
            }
            return false;
            
        }

        private static string GenerateRandomOtp(int length)
        {
            var random = new Random();
            return string.Concat(Enumerable.Range(0, length).Select(_ => random.Next(0, 10).ToString()));
        }
    }
}
