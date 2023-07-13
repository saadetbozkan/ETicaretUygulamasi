using ETicaretAPI.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ETicaretAPI.Infrastructure.Services
{
    public class MailService : IMailService
    {
        readonly IConfiguration config;

        public MailService(IConfiguration config)
        {
            this.config = config;
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
           await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
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

        public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        {
            StringBuilder mail = new();
            mail.Append("Merhaba,<br>Eğer yeni şifre talebinde bulunduysanız aşağıdaki linketen şifrenizi yenileyebilirsiniz.<br><strong> <a target=\"_blank\" href=\"");
            mail.Append(this.config["AngularClientUrl"]);
            mail.Append("/update-password/");  
            mail.Append(userId);
            mail.Append("/");
            mail.Append(resetToken);
            mail.Append("\">Yeni şifre talebi için tıklayınız...</a></strong><br><br><span style=\"font-size:12px;\"> NOT: Şifre değiştirme talebiniz yoksa bu maili ciddiye almayınız.</span><br>Saygılarımızla...<br><br><br> - Mini E-Ticaret -");

            await SendMailAsync(to, "Şifre Yenileme Talebi", mail.ToString());

        }

        public async Task SendComplatedOrderMailAsync(string to, string orderCode, DateTime orderDate, string userNameSurname)
        {
            string mail = $"Sayın {userNameSurname} Merhaba<br> {orderDate} tarihinde vermiş olduğunuz {orderCode} kodlu şiparişiniz tamamlanmış ve kargoya verilmiştir.<br> Bizi tercih ettiğiniz için teşekkür ederiz.";
            await SendMailAsync(to, $"{orderCode} Numaralı Şiparişiniz Tamamlandı!",mail);
        }
    }
}
