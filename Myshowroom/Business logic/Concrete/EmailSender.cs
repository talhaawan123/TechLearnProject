using MailKit.Net.Smtp;
using MimeKit;
namespace TechLearn.Business_logic.Concrete
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
    public class EmailSender : IEmailSender
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailSender(IConfiguration configuration)
        {
            _smtpSettings = configuration.GetSection("SmtpSettings").Get<SmtpSettings>();
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("html") { Text = message };

            using (var client = new SmtpClient())
            {
                client.Connect(_smtpSettings.Server, _smtpSettings.Port, false);
                client.Authenticate(_smtpSettings.Username, _smtpSettings.Password);

                await client.SendAsync(emailMessage);
                client.Disconnect(true);
            }
        }
    }
}
