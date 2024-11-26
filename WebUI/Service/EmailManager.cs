using System.Net;
using System.Net.Mail;
using System.Text;

namespace WebUI.Service
{
    public class EmailManager : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void EmailSender(string email, string body, string subject)
        {
            SmtpClient smtpClient = new SmtpClient(_configuration["EmailSetting:Host"], int.Parse(_configuration["EmailSetting:Port"]));
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(_configuration["EmailSetting:Email"], _configuration["EmailSetting:Password"]);

            MailMessage mailMessage = new();
            mailMessage.From = new MailAddress(_configuration["EmailSetting:Email"]);
            mailMessage.To.Add(email);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            
            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat("<h1>User Confirmation</h1>");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat($"<p>Thank you For Registering account. For confirmation click the link: <a href='{body}'>Click!</a> </p>");
            mailMessage.Body = mailBody.ToString();

            smtpClient.Send(mailMessage);
        }
    }
}
