namespace MkExpress.MessageBroker.Services
{
    public interface ITemplateService
    {
        string GetEmailTemplate(string templateName, params object[] args);
    }
}
