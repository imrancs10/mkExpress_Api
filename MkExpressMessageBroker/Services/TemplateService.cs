using Microsoft.Extensions.Configuration;

namespace MkExpress.MessageBroker.Services
{
    public class TemplateService: ITemplateService
    {
        private readonly IConfiguration _configuration;

        public TemplateService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetEmailTemplate(string templateName, params object[] args)
        {
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates", $"{templateName}.html");

            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException($"Template {templateName} not found at {templatePath}");
            }

            var templateContent = File.ReadAllText(templatePath);
            return string.Format(templateContent, args);
        }
    }
}
