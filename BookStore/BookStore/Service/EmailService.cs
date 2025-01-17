using BookStore.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service
{
    public class EmailService : IEmailService
    {
        private const string templatePath = @"EmailTemplate/{0}.html";
        private readonly SMTPConfgiModel _smtpConfig;
        public async Task SendTestEmail(UserEmailOptions options)
        {
            options.Subject = UpdatePlaceHolders("Hello {{UserName}},This is test email subject from book store web app", options.PlaceHolders);
            options.Body = UpdatePlaceHolders(GetEmailBody("TestEmail"), options.PlaceHolders);
            await SendEmail(options);
        }
        public async Task SendConfirmedEmail(UserEmailOptions options)
        {
            options.Subject = UpdatePlaceHolders("Hello {{UserName}},Confirmed your email id.", options.PlaceHolders);
            options.Body = UpdatePlaceHolders(GetEmailBody("ConfirmEmail"), options.PlaceHolders);
            await SendEmail(options);
        }
        public async Task SendEmailForForgotPassword(UserEmailOptions options)
        {
            options.Subject = UpdatePlaceHolders("Hello {{UserName}},reset your password.", options.PlaceHolders);
            options.Body = UpdatePlaceHolders(GetEmailBody("ForgotPassword"), options.PlaceHolders);
            await SendEmail(options);
        }
        public EmailService(IOptions<SMTPConfgiModel> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }
        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            MailMessage mail = new MailMessage
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHtml
            };
            foreach (var item in userEmailOptions.ToEmails)
            {
                mail.To.Add(item);
            }
            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);
            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UserDefaultCredentials,
                Credentials = networkCredential,
            };
            mail.BodyEncoding = Encoding.Default;
            await smtpClient.SendMailAsync(mail);
        }
        private string GetEmailBody(string templateName)
        {
            var body = System.IO.File.ReadAllText(string.Format(templatePath, templateName));
            return body;
        }
        private string UpdatePlaceHolders(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if (!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach (var pair in keyValuePairs)
                {
                    if(text.Contains(pair.Key))
                    {
                        text = text.Replace(pair.Key, pair.Value);
                    }
                }
            }
            return text;
        }
    }
}
