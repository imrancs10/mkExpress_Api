namespace MkExpress.MessageBroker.Services
{
    public interface INotificationService
    {
        Task SendSmsAsync(string phoneNumber, string message);
        Task SendEmailAsync(string toEmail, string subject, string body);
        Task SendTemplateEmailAsync(string toEmail, string templateName, params object[] args);
    }
}
