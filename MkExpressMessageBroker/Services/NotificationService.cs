namespace MkExpress.MessageBroker.Services
{
    public class NotificationService:INotificationService
    {
        private readonly SemaphoreSlim _smsSemaphore = new(5); // Example: Allow 5 concurrent SMS messages
        private readonly SemaphoreSlim _emailSemaphore = new(10); // Example: Allow 10 concurrent email messages

        private readonly ISmsService _smsService;
        private readonly IEmailService _emailService;

        public NotificationService(ISmsService smsService, IEmailService emailService)
        {
            _smsService = smsService;
            _emailService = emailService;
        }

        public async Task SendSmsAsync(string phoneNumber, string message)
        {
            await _smsSemaphore.WaitAsync();
            try
            {
                await _smsService.SendSmsAsync(phoneNumber, message);
            }
            finally
            {
                _smsSemaphore.Release();
            }
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            await _emailSemaphore.WaitAsync();
            try
            {
                await _emailService.SendEmailAsync(toEmail, subject, body);
            }
            finally
            {
                _emailSemaphore.Release();
            }
        }

        public async Task SendTemplateEmailAsync(string toEmail, string templateName, params object[] args)
        {
            await _emailSemaphore.WaitAsync();
            try
            {
                var emailServiceWithTemplates = _emailService as EmailService;
                if (emailServiceWithTemplates != null)
                {
                    await emailServiceWithTemplates.SendTemplateEmailAsync(toEmail, templateName, args);
                }
            }
            finally
            {
                _emailSemaphore.Release();
            }
        }
    }
}
