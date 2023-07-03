using ETicaretAPI.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace ETicaretAPI.Infrastructure.Services
{
    public class MailService : IMailService
    {
        readonly IConfiguration config;

        public MailService(IConfiguration config)
        {
            this.config = config;
        }

        public async Task SendMessageAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
           await SendMessageAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMessageAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;
            foreach(var to in tos)
               mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new(config["Mail:Username"], " E-Ticaret", System.Text.Encoding.UTF8);
            SmtpClient smtp = new();
            smtp.Credentials = new NetworkCredential(config["Mail:Username"], config["Mail:Password"]);
            smtp.Port = Convert.ToInt32(config["Mail:Port"]);
            smtp.EnableSsl = true;
            smtp.Host = config["Mail:Host"];
            await smtp.SendMailAsync(mail);
        }
    }
}
