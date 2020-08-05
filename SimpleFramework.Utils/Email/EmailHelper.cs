using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SimpleFramework.Utils.Email
{
    public class EmailHelper
    {
        public bool SendMail(EmailInfo emailInfo)
        {
            Validation(emailInfo);

            var message = new MailMessage
            {
                IsBodyHtml = emailInfo.UseHtml,
                From = new MailAddress(emailInfo.From, emailInfo.FromName)
            };

            message.Subject = emailInfo.Subject;
            message.Body = emailInfo.Body;

            foreach (var repicient in emailInfo.Recipients)
            {
                message.To.Add(new MailAddress(repicient));
            }

            foreach (var recipientCc in emailInfo.RecipientsCc)
            {
                message.To.Add(new MailAddress(recipientCc));
            }

            foreach (var attachments in emailInfo.EmailInfoAttachments)
            {
                message.Attachments.Add(new Attachment(new MemoryStream(
                                                    attachments.AttachmentFile), 
                                                    attachments.AttachmentName, 
                                                    attachments.AttachmentMediaType));
            }

            using (var client = new SmtpClient(emailInfo.Host, emailInfo.Port))
            {
                client.Credentials = new NetworkCredential(emailInfo.CredentialUser, emailInfo.CredentialPassword);
                client.EnableSsl = emailInfo.UseSsl;
                try
                {
                    client.Send(message);
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<bool> SendMailAsync(EmailInfo emailInfo)
        {
            Validation(emailInfo);

            var message = new MailMessage
            {
                IsBodyHtml = emailInfo.UseHtml,
                From = new MailAddress(emailInfo.From, emailInfo.FromName)
            };

            message.Subject = emailInfo.Subject;
            message.Body = emailInfo.Body;

            foreach (var repicient in emailInfo.Recipients)
            {
                message.To.Add(new MailAddress(repicient));
            }

            foreach (var recipientCc in emailInfo.RecipientsCc)
            {
                message.To.Add(new MailAddress(recipientCc));
            }

            foreach (var attachments in emailInfo.EmailInfoAttachments)
            {
                message.Attachments.Add(new Attachment(new MemoryStream(
                                                    attachments.AttachmentFile),
                                                    attachments.AttachmentName,
                                                    attachments.AttachmentMediaType));
            }

            using (var client = new SmtpClient(emailInfo.Host, emailInfo.Port))
            {
                client.Credentials = new NetworkCredential(emailInfo.CredentialUser, emailInfo.CredentialPassword);
                client.EnableSsl = emailInfo.UseSsl;
                try
                {
                    await client.SendMailAsync(message);
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        private void Validation(EmailInfo emailInfo)
        {
            var errors = new List<Exception>();

            if(!new EmailAddressAttribute().IsValid(emailInfo.From))
            {
                errors.Add(new Exception("The email is invalid."));
            }

            if (string.IsNullOrWhiteSpace(emailInfo.FromName))
            {
                errors.Add(new Exception("FromName cannot be null."));
            }

            if (string.IsNullOrWhiteSpace(emailInfo.CredentialUser))
            {
                errors.Add(new Exception("CredentialUser cannot be null."));
            }

            if (string.IsNullOrWhiteSpace(emailInfo.CredentialPassword))
            {
                errors.Add(new Exception("CredencialPassword cannot be null."));
            }

            if (string.IsNullOrWhiteSpace(emailInfo.Subject))
            {
                errors.Add(new Exception("Subject cannot be null."));
            }

            var listRecipients = emailInfo.Recipients.ToList();

            if (!listRecipients.Any())
            {
                errors.Add(new Exception("Recipients cannot be null."));
            }

            if(listRecipients.Any(x=> !new EmailAddressAttribute().IsValid(emailInfo.From)))
            {
                errors.Add(new Exception("One or more recipients has invalid email."));
            }

            var listRecipientsCc = emailInfo.RecipientsCc.ToList();

            if (listRecipientsCc.Any(x => !new EmailAddressAttribute().IsValid(emailInfo.From)))
            {
                errors.Add(new Exception("One or more recipients with CC has invalid email."));
            }

            var attachments = emailInfo.EmailInfoAttachments.ToList();

            if(attachments.Any(x=> string.IsNullOrWhiteSpace(x.AttachmentName)))
            {
                errors.Add(new Exception("One or more attachments is unnamed."));
            }

            if (attachments.Any(x => string.IsNullOrWhiteSpace(x.AttachmentMediaType)))
            {
                errors.Add(new Exception("One or more attachments is missing Media Type."));
            }

            if(attachments.Any(x=> x.AttachmentFile == null) || attachments.Any(x=>x.AttachmentFile.Length <= 0))
            {
                errors.Add(new Exception("One or more files have not been loaded."));
            }

            throw new AggregateException(errors);
        }
    }
}
