using Microsoft.AspNetCore.Mvc;
using WebCourierApi.Data;
using WebCourierApi.Data.Repositories;
using WebCourierApi.Model.Entities;
using WebCourierApi.Utils.MailService;
using WebCourierHub.Classes;

namespace WebCourierApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsletterController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly INewsletterRepository _newsletterRepository;

        public NewsletterController(IEmailService emailService, INewsletterRepository newsletterRepository)
        {
            _emailService = emailService;
            _newsletterRepository = newsletterRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MailDto mail)
        {
            if (mail.Mail.Length < 5 || !mail.Mail.Contains("@"))
            {
                return BadRequest("Mail must be valid and contain @");
            }

            await _newsletterRepository.AddNewsletterAsync(mail.Mail);

            await _emailService.SendEmailAsync(mail.Mail, "WebCourierHub newsletter", $"Hello, \n your have been signed for newsletter :). \n thank you");
            return Ok("You have been added to the newsletter");
        }

        [HttpGet("unsubscribe/{email}")]
        public async Task<IActionResult> Unsubscribe(string email)
        {
            if (!email.Contains("@"))
            {
                return BadRequest("Invalid email format");
            }

            // Check if the email exists in your database
            var subscriber = await _newsletterRepository.GetSubscriberByEmailAsync(email);
            if (subscriber == null)
            {
                return NotFound("Subscriber not found");
            }
            await _newsletterRepository.RemoveSubscriberAsync(subscriber);
            await _emailService.SendEmailAsync(subscriber.Mail, "WebCourierHub newsletter", $"Hello, \n why did you unsubscribe our newsletter??????? :( \n \n \np\nl\ne\na\ns\ne\nd\no\nn\nt\nd\no\ni\nt\n");

            return Ok("You have been unsubscribed successfully");
        }
    }
}