using System;
using System.Collections.Generic;

namespace csharp_test_hopper.Models
{
    /// <summary>
    /// Representation of an email in the DB
    /// </summary>
    /// <remarks>
    /// Result can be either "Failed or "OK",
    /// FailedMessage stores the Exception.Message if the email
    /// could not be sent.
    /// CreatedAt is in UTC.
    /// </remarks>
    public class EmailEntry
    {
        public long Id { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string MailFrom { get; set; }

        public string Result { get; set; }

        public string FailedMessage { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<Recipient> Recipients { get; set; }
    }
}
