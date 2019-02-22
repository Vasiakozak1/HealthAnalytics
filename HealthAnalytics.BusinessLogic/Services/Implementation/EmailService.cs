using HealthAnalytics.BusinessLogic.Services.Abstract;
using Microsoft.Extensions.Configuration;
using MimeKit;
using RazorLight;
using System.IO;
using System;
using System.Threading.Tasks;
using HealthAnalytics.BusinessLogic.Data.Models;
using HealthAnalytics.Data.Entities;
using MailKit.Net.Smtp;

namespace HealthAnalytics.BusinessLogic.Services.Implementation
{
    public class EmailService: IEmailService
    {
        private readonly string fromEmailAddress;
        private readonly string fromEmailAddressPassword;
        private readonly string fromEmailName;
        private readonly string smtpAddress;
        private readonly int smtpPort;
        private readonly IRazorLightEngine razorEngine;

        public EmailService(IConfiguration configuration)
        {
            IConfigurationSection emailSection = configuration.GetSection("Email");
            fromEmailAddress = emailSection[nameof(fromEmailAddress)];
            fromEmailAddressPassword = emailSection[nameof(fromEmailAddressPassword)];
            fromEmailName = emailSection[nameof(fromEmailName)];
            smtpAddress = emailSection[nameof(smtpAddress)];
            smtpPort = int.Parse(emailSection[nameof(smtpPort)]);
            string pathToTemplates = Path.Combine(Directory.GetCurrentDirectory(), Constants.TEMPLATES_FOLDER_NAME);
            razorEngine = EngineFactory.CreatePhysical(pathToTemplates);
        }

        public async Task SendEmailConfirmationMessage<TKey>(User<TKey> user, string confirmationUrl) where TKey: struct, IComparable<TKey>
        {
            var confirmEmailModel = new ConfirmEmailModel
            {
                ConfirmationUrl = confirmationUrl,
                ExpirationDate = DateTime.Now.AddHours(Constants.USER_REGISTER_TOKEN_EXPIRE_HOURS),
                ReceiverEmail = user.Email,
                ReceiverFirstName = user.FirstName,
                ReceiverLastName = user.LastName
            };

            string messageText = getConfirmEmailMessage(confirmEmailModel);
            await sendEmailAsync(messageText, Constants.CONFIRM_EMAIL_MESSAGE_SUBJECT, user.Email);
        }

        private async Task sendEmailAsync(string messageText, string subject, string receiverAddress) 
        {
            var message = new MimeMessage();
            message.Importance = MessageImportance.High;
            message.From.Add(new MailboxAddress(fromEmailName, fromEmailAddress));
            message.To.Add(new MailboxAddress(receiverAddress));
            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = messageText
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpAddress, smtpPort, false);
                await client.AuthenticateAsync(fromEmailAddress, fromEmailAddressPassword);
                await client.SendAsync(message);

                await client.DisconnectAsync(quit: true);
            }
        }

        private string getConfirmEmailMessage(ConfirmEmailModel model)
        {
            return createMessageBody(Constants.CONFIRM_EMAIL_TEMPLATE_NAME, model);
        }

        private string createMessageBody<TModel>(string templateName, TModel model) where TModel : class
        {
            return razorEngine.Parse(templateName, model);
        }
    }
}
