namespace SimpleFramework.Utils.Email
{
    public class EmailInfo
    {
        public string From { get; set; }

        public string Subject { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public bool UseSsl { get; set; }

        public bool UseHtml { get; set; }
    }
}
