namespace Portfolio.Web.Contact
{
    public class ContactOptions
    {
        public const string SectionName = "Contact";

        public string To { get; set; }
        public string From { get; set; }
        public string FromName { get; set; } = "dimitar-nikolov.net";
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; } = 587;
        public string SmtpUser { get; set; }
        public string SmtpPassword { get; set; }

        public bool IsConfigured =>
            !string.IsNullOrWhiteSpace(To) && !string.IsNullOrWhiteSpace(From) &&
            !string.IsNullOrWhiteSpace(SmtpHost) && !string.IsNullOrWhiteSpace(SmtpUser) &&
            !string.IsNullOrWhiteSpace(SmtpPassword);
    }
}
