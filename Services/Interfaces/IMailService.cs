using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
        Task<string> GetMailTemplete(EmailTemplateEnum emailTemplateEnum);
    }
}
