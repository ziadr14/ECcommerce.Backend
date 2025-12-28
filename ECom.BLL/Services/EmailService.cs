using ECom.BLL.DTOs;
using ECom.BLL.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Threading.Tasks;


namespace ECom.BLL.Services
{
    public class EmailService : IEmailService
    {

        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(EmailDto emailDto)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("My ECom", _configuration["EmailSetting:From"]));
            message.To.Add(MailboxAddress.Parse(emailDto.To));
            message.Subject = emailDto.Subject;

            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailDto.Content
            };

            using var client = new MailKit.Net.Smtp.SmtpClient();

            await client.ConnectAsync(
                _configuration["EmailSetting:Smtp"],
                int.Parse(_configuration["EmailSetting:Port"]),
                MailKit.Security.SecureSocketOptions.SslOnConnect
            );

            await client.AuthenticateAsync(
                _configuration["EmailSetting:UserName"],
                _configuration["EmailSetting:Password"]
            );

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        public async Task SendEmailAsync(ContactMessageDto dto)
        {
            var emailSettings = _configuration.GetSection("EmailSetting");

            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(emailSettings["From"]));
            message.To.Add(MailboxAddress.Parse(emailSettings["From"])); 
            message.Subject = dto.Subject;

            message.Body = new TextPart("html")
            {
                Text = $@"
                <h2>New Contact Message</h2>
                <p><b>Name:</b> {dto.Name}</p>
                <p><b>Email:</b> {dto.Email}</p>
                <p><b>Message:</b><br/>{dto.Message}</p>
            "
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(
                emailSettings["Smtp"],
                int.Parse(emailSettings["Port"]),
                SecureSocketOptions.SslOnConnect
            );

            await smtp.AuthenticateAsync(
                emailSettings["UserName"],
                emailSettings["Password"]
            );

            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }




public async Task SendEmailWithAttachmentAsync(
    string to,
    string subject,
    string body,
    byte[] attachmentBytes,
    string fileName
)
    {
        var emailSettings = _configuration.GetSection("EmailSetting");

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("My ECom", emailSettings["From"]));
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;

        var builder = new BodyBuilder
        {
            HtmlBody = body
        };

        builder.Attachments.Add(
            fileName,
            attachmentBytes,
            ContentType.Parse("application/pdf")
        );

        message.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient(); 
        await smtp.ConnectAsync(
            emailSettings["Smtp"],
            int.Parse(emailSettings["Port"]),
            SecureSocketOptions.SslOnConnect
        );

        await smtp.AuthenticateAsync(
            emailSettings["UserName"],
            emailSettings["Password"]
        );

        await smtp.SendAsync(message);
        await smtp.DisconnectAsync(true);
    }



}
}
