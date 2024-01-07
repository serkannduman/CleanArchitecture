using CleanArchitecture.Application.Services;
using System.Net.Mail;
using GenericEmailService;

namespace CleanArchitecture.Infrastructure.Services;

public sealed class MailService : IMailService
{
    public async Task SendMailAsync(IList<string> emails,string subject,string body, List<Attachment>? attachments)
    {
        SendEmailModel sendEmailModel = new()
        {
            Body = body,
            Attachments = attachments,
            Email = "",
            Html = true,
            Password = "",
            Port = 587,
            Smtp = "",
            SSL = true,
            Subject = subject,
        };

        await EmailService.SendEmailAsync(sendEmailModel);
        throw new NotImplementedException();
    }
}
