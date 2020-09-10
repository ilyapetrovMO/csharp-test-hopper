using System.Threading.Tasks;

namespace csharp_test_hopper.Util
{
    public interface IEmailService
    {
         Task SendEmailAsync(
            string subject,
            string body,
            string[] recipients,
            string mailFrom);
    }
}
