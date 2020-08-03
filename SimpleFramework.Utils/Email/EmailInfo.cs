using iTextSharp.text;
using System.Collections.Generic;

namespace SimpleFramework.Utils.Email
{
    public class EmailInfo
    {
        public string From { get; set; }

        public string FromName { get; set; }

        public IEnumerable<string> Recipients { get; set; }

        public IEnumerable<string> RecipientsCc { get; set; }

        public string Subject { get; set; }

        public string Host { get; set; }

        public string Body { get; set; }

        public int Port { get; set; }

        public bool UseSsl { get; set; }

        public bool UseHtml { get; set; }

        public List<byte[]> Attachments { get; set; }
        public string CredentialUser { get; set; }
        public string CredentialPassword { get; set; }
    }
}
