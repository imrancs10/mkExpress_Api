namespace MkExpress.MessageBroker.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string toEmail, string subject, string body);
        Task<bool> SendTemplateEmailAsync(string toEmail, string templateName, params object[] args);
    }
}
