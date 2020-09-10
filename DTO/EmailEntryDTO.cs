using System.ComponentModel.DataAnnotations;

namespace csharp_test_hopper.DTO
{
    /// <summary>
    /// The object used to get the required data for sending an email
    /// that provides validation and hides fields that the user should not fill themselves (like CreatedAt)
    /// </summary>
    public class EmailEntryDTO
    {
        public string Subject { get; set; } = string.Empty;

        [Required]
        public string Body { get; set; }

        [Required]
        public string[] Recipients { get; set; }

        [Required]
        public string MailFrom { get; set; }
    }
}