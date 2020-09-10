namespace csharp_test_hopper.Util
{
    /// <summary>
    /// Class used for storing all SMTP configuration options.
    /// </summary>
    public class EmailConfig
    {
        public string FromName { get; set; }

        public string FromAddress { get; set; }

        public string MailServerAddress { get; set; }

        public string MailServerPort { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }
    }
}
