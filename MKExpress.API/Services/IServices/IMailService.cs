using MKExpress.API.Dto.Request;
using MKExpress.API.Constants;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
        Task<string> GetMailTemplete(EmailTemplateEnum emailTemplateEnum);
    }
}
