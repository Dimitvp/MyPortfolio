using System.ComponentModel.DataAnnotations;

namespace Portfolio.Web.Contact
{
    public class ContactMessage
    {
        [Required(ErrorMessage = "Please enter your name.")]
        [StringLength(100)]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "Please enter your email.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(200)]
        public string ContactEmail { get; set; }

        [StringLength(200)]
        public string ContactSubject { get; set; }

        [Required(ErrorMessage = "Please enter a message.")]
        [StringLength(5000, MinimumLength = 10, ErrorMessage = "The message must be between 10 and 5000 characters.")]
        public string ContactMessageText { get; set; }

        // Honeypot: hidden from people, filled in by naive bots.
        public string Website { get; set; }

        public bool IsFromBot => !string.IsNullOrEmpty(Website);
    }
}
