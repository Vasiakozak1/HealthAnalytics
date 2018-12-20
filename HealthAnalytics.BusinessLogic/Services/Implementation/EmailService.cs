using HealthAnalytics.BusinessLogic.Services.Abstract;
using Microsoft.Extensions.Configuration;
using MimeKit;
using RazorLight;
using System.IO;

namespace HealthAnalytics.BusinessLogic.Services.Implementation
{
    public class EmailService: IEmailService
    {
        private readonly string fromEmailAddress;
        private readonly string fromEmailAddressPassword;
        private readonly string fromEmailName;
        private readonly string smtpAddress;
        private readonly string smtpPort;

        private readonly string pathToTemplates;

        public EmailService(IConfiguration configuration)
        {
            IConfigurationSection emailSection = configuration.GetSection("Email");
            fromEmailAddress = emailSection[nameof(fromEmailAddress)];
            fromEmailAddressPassword = emailSection[nameof(fromEmailAddressPassword)];
            fromEmailName = emailSection[nameof(fromEmailName)];
            smtpAddress = emailSection[nameof(smtpAddress)];
            smtpPort = emailSection[nameof(smtpPort)];
            pathToTemplates = Path.Combine(Directory.GetCurrentDirectory(), Constants.TEMPLATES_FOLDER_NAME);
        }

        public void SendEmail(string text, string subject, string receiverAddress)
        {
            var message = new MimeMessage();
            message.Importance = MessageImportance.High;
            message.From.Add(new MailboxAddress(fromEmailName, fromEmailAddress));
            message.To.Add(new MailboxAddress(receiverAddress));
            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                
            };
        }

        //private string createMessageBody<TModel>(string templateName, TModel model) where TModel: class
        //{
        //    IRazorLightEngine razorEngine = EngineFactory.CreatePhysical()
        //}
    }
}
