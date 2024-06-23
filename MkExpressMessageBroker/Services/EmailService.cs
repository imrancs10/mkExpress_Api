using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Newtonsoft.Json;
using System.Reflection;

namespace MkExpress.MessageBroker.Services
{
    public class EmailService : IEmailService
    {
        private readonly GmailService _gmailService;
        private readonly string _fromEmail;
        private readonly ITemplateService _templateService;
        static string[] Scopes = { GmailService.Scope.GmailSend };
        static string ApplicationName = "IMK EXPRESS";

        public EmailService(IConfiguration configuration, ITemplateService templateService)
        {
            //_fromEmail = configuration["Google:FromEmail"];
            //var _exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace(@"file:\", "");
            //UserCredential credential;

            //using (var stream = new FileStream(_exePath + "\\googleOAuthCredential.json", FileMode.Open, FileAccess.Read))
            //{
            //    string credPath = "token.json";
            //    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            //        GoogleClientSecrets.Load(stream).Secrets,
            //        Scopes,
            //        "user",
            //        CancellationToken.None,
            //        new FileDataStore(credPath, true)).Result;
            //    Console.WriteLine("Credential file saved to: " + credPath);
            //}

            //// Create Gmail API service.
            //_gmailService = new GmailService(new BaseClientService.Initializer()
            //{
            //    HttpClientInitializer = credential,
            //    ApplicationName = ApplicationName,
            //});

            _templateService = templateService;
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Sender", _fromEmail));
                message.To.Add(new MailboxAddress("Recipient", toEmail));
                message.Subject = subject;
                message.Body = new TextPart("html")
                {
                    Text = body
                };

                var gmailMessage = new Google.Apis.Gmail.v1.Data.Message
                {
                    Raw = Base64UrlEncode(message.ToString())
                };

                await _gmailService.Users.Messages.Send(gmailMessage, "me").ExecuteAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log exception or handle as needed
                return false;
            }
        }

        public async Task<bool> SendTemplateEmailAsync(string toEmail, string templateName, params object[] args)
        {
            string body = _templateService.GetEmailTemplate(templateName, args);
            string subject = templateName.Replace("Template", ""); // Example subject
            return await SendEmailAsync(toEmail, subject, body);
        }

        private static string Base64UrlEncode(string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes).Replace('+', '-').Replace('/', '_').Replace("=", "");
        }
    }
}
