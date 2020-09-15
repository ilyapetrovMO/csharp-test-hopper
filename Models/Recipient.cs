using System.ComponentModel.DataAnnotations;

namespace csharp_test_hopper.Models
{
    public class Recipient
    {
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
    }
}