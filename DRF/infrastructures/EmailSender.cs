using DRF.Utilities;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;

namespace DRF.infrastructures
{
    public class EmailSender : ICustomEmailSender
    {
        private string host;
        private int port;
        private bool enableSSL;
        private string userName;
        private string password;
        private string from;
        private string fromDisplayName;
        private readonly IWebHostEnvironment hostingEnvironment;
        public EmailSender(string host, int port, bool enableSSL, string userName, string password, IWebHostEnvironment hostingEnvironment, string from, string fromDisplayName)
        {
            this.host = host;
            this.port = port;
            this.enableSSL = enableSSL;
            this.userName = userName;
            this.password = AESCryptoProvider.GetInstnace.Decrypt(password);
            this.hostingEnvironment = hostingEnvironment;
            this.from = from;
            this.fromDisplayName = fromDisplayName;
        }
        public Task SendHtmlEmail(string[] emails, string[] cc, string[] bcc, string subject, HtmlMessageEnum type, Dictionary<string, string> param, List<EmailAttachmentModel> attachments = null)
        {
            string body = string.Empty;
            switch (type)
            {
            
                case HtmlMessageEnum.CHANGE_PASSWORD:
                    body = RenderHtmlMessage("change-password.html", param);
                    break;
                case HtmlMessageEnum.CREATE_ACCOUNT:
                    body = RenderHtmlMessage("create-account.html", param);
                    break;
                case HtmlMessageEnum.FORGET_PASSWORD:
                    body = RenderHtmlMessage("password-reset.html", param);
                    break;
                default:
                    body = string.Empty;
                    break;
            }
            if (!string.IsNullOrEmpty(body))
            {
                return SendEmailAsync(emails, cc, bcc, subject, body, attachments);
            }
            return null;
        }

        public Task SendEmailAsync(string[] email, string[] cc, string[] bcc, string subject, string htmlMessage, List<EmailAttachmentModel> attachments = null)
        {
            var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = enableSSL
            };


            var mail = new MailMessage()
            {
                From = new MailAddress(from, fromDisplayName),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true,
                Priority = MailPriority.High
            };
            if (attachments != null && attachments.Count > 0)
            {
                foreach (var item in attachments)
                {
                    MemoryStream strm = new MemoryStream(item.Content);
                    Attachment data = new Attachment(strm, item.FileName);
                    ContentDisposition disposition = data.ContentDisposition;
                    data.ContentId = item.FileName;
                    data.ContentDisposition.Inline = true;
                    mail.Attachments.Add(data);
                }
            }

            if (cc != null && cc.Length > 0)
            {
                var ccList = cc.Distinct();
                foreach (var em in ccList)
                {
                    try
                    {
                        mail.CC.Add(em);
                    }
                    catch
                    {

                    }
                }
            }

            if (bcc != null && bcc.Length > 0)
            {
                var bccList = bcc.Distinct();
                foreach (var em in bccList)
                {
                    try
                    {
                        mail.Bcc.Add(em);
                    }
                    catch
                    {

                    }
                }
            }

            var toList = email.Distinct();
            foreach (var em in toList)
            {
                try
                {
                    mail.To.Add(em);
                }
                catch
                {

                }
            }

            return client.SendMailAsync(mail);
        }
        private string RenderHtmlMessage(string htmlfile, Dictionary<string, string> param)
        {
            StringBuilder body = new StringBuilder();
            using (StreamReader reader = new StreamReader(Path.Combine(hostingEnvironment.WebRootPath, "html_message/" + htmlfile)))
            {
                body.Append(reader.ReadToEnd());
            }
            foreach (var p in param)
            {
                body.Replace("{#" + p.Key + "}", p.Value);
            }
            return body.ToString();
        }


    }
}
