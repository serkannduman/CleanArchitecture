using System.Net.Mail;

namespace CleanArchitecture.Application.Services;

public interface IMailService
{
    Task SendMailAsync(IList<string> emails, string subject, string body, List<Attachment>? attachments);
}
