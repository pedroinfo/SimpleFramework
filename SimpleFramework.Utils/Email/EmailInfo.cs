using System.Collections.Generic;

namespace SimpleFramework.Utils.Email
{
    public class EmailInfo
    {
        public string Host { get; set; }
        public string CredentialUser { get; set; }
        public string CredentialPassword { get; set; }
        public int Port { get; set; }
        public string From { get; set; }
        public string FromName { get; set; }
        public bool UseSsl { get; set; }
        public bool UseHtml { get; set; }
        public IEnumerable<string> Recipients { get; set; }
        public IEnumerable<string> RecipientsCc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public IEnumerable<EmailInfoAttachment> EmailInfoAttachments { get; set; }
    }

    public class EmailInfoAttachment
    {
        public string AttachmentName { get; set; }
        public byte[] AttachmentFile { get; set; }
        public string AttachmentMediaType { get; set; }
    }
}
