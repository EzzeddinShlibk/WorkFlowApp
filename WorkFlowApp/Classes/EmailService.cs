using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Threading.Tasks;

public class EmailService
{
    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress("WorFlow", "nusaibamilad@gmail.com"));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = subject;
        
        // Create a body part
        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = message;

        emailMessage.Body = bodyBuilder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync("nusaibamilad@gmail.com", "tgiczjpidrkmgphd");
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
