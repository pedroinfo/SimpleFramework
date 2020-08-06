using SimpleFramework.Utils.Email;
using System.Collections.Generic;

namespace SimpleFramework.ConsoleTests
{
    public class MailTest
    {
        public void Tests()
        {
            var emailHelper = new EmailHelper();
            emailHelper.SendMail(new EmailInfo()
            {
                CredentialUser = "", 
                CredentialPassword = "", 
                From = "", 
                FromName = "",
                Body = "",
                Subject = "", 
                Host = "", 
                Port = 999,
                UseHtml = true, 
                UseSsl = true,
                Recipients = new List<string> { "" },
                EmailInfoAttachments = new List<EmailInfoAttachment> 
                { 
                    new EmailInfoAttachment
                    {
                        AttachmentFile = null,
                        AttachmentMediaType = "", 
                        AttachmentName = ""
                    }
                }
            });


        }
    }
}
