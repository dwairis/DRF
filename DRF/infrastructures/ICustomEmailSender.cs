using DRF.Utilities;
using DRF.ViewModels;

namespace DRF.infrastructures
{
    public interface ICustomEmailSender
    {
        Task SendHtmlEmail(string[] email, string[] cc, string[] bcc, string subject, HtmlMessageEnum type, Dictionary<string, string> param, List<EmailAttachmentModel> attachments = null);
        Task SendEmailAsync(string[] email, string[] cc, string[] bcc, string subject, string htmlMessage, List<EmailAttachmentModel> attachments = null);
    }
}
