using System.Net;
using System.Net.Mail;
using Infrastructure.Common;
using Infrastructure.Common.Email;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

public class EmailService: IMessageService
{
    private readonly IOptions<EmailOptions> _emailOptions;

    public EmailService(IOptions<EmailOptions> emailOptions)
    {
        _emailOptions = emailOptions;
    }

    public async Task<bool> SendAsync(Email email)
    {
        using var client = new SmtpClient();

        client.Credentials = new NetworkCredential(_emailOptions.Value.EmailAddress, _emailOptions.Value.Password);
        client.Host = _emailOptions.Value.Host;
        client.Port = _emailOptions.Value.Port;
        client.EnableSsl = _emailOptions.Value.EnableSsl;
        client.UseDefaultCredentials = _emailOptions.Value.UseDefaultCredentials;

        var message = new MailMessage();
        message.From = new MailAddress(_emailOptions.Value.EmailAddress);
        message.To.Add(new MailAddress(email.To));
        message.Subject = email.Subject;
        message.Body = email.Body;
        try
        {
            await client.SendMailAsync(message);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }
}