using System;
using System.Linq;
using System.Threading.Tasks;
using csharp_test_hopper.Models;
using Microsoft.AspNetCore.Mvc;
using csharp_test_hopper.Util;
using csharp_test_hopper.DTO;

namespace csharp_test_hopper.Controllers
{
    [Route("api/mail")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        /// <value>DbContext for the email log DB</value>
        private EmailContext Context { get; }

        /// <value>email sender service</value>
        private IEmailService EmailService { get; }

        public EmailController(EmailContext context, IEmailService emailService)
        {
            Context = context;
            EmailService = emailService;
        }

        /// <summary>
        /// On http GET returns all of the logged emails.
        /// </summary>
        /// <returns>JSON List of emailEntry </returns>
        [HttpGet]
        public IActionResult GetEmail()
        {
            try
            {
                var mail = Context.Emails.ToList();
                return Ok(mail);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }
        
        /// <summary>
        /// On http POST expects at least three of the mailEntryDTO fields,
        /// converts to an EmailEntry object and attempts to send the email using the emailService,
        /// after which logs the attempt in the database via Context.
        /// </summary>
        /// <param name="mailEntryDTO"></param>
        /// <returns>
        /// BAD REQUEST - if validation isnt passed
        /// 500 - if something went wrong while sending the mail
        /// 200 and the recieved JSON object if the email is sent.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> SendEmail(EmailEntryDTO mailEntryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var mailEntry = new EmailEntry()
            {
                Subject = mailEntryDTO.Subject,
                Body = mailEntryDTO.Body,
                Recipients = mailEntryDTO.Recipients,
                MailFrom = mailEntryDTO.MailFrom,
                Result = "OK"
            };

            try
            {
                await EmailService.SendEmailAsync(
                    mailEntryDTO.Subject,
                    mailEntryDTO.Body,
                    mailEntryDTO.Recipients,
                    mailEntryDTO.MailFrom);
                await Context.Emails.AddAsync(mailEntry);
                return Ok(mailEntryDTO);
            }
            catch (Exception ex)
            {
                mailEntry.Result = "Failed";
                mailEntry.FailedMessage = ex.Message;
                await Context.Emails.AddAsync(mailEntry);

                return StatusCode(500);     
            }
        }
    }
}
