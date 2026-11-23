using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Logging;
using Portfolio.Web.Contact;

namespace Portfolio.Web.Controllers
{
    public class ContactController : Controller
    {
        public const string RateLimitPolicy = "contact";

        private readonly IContactMailer _mailer;
        private readonly ILogger<ContactController> _logger;

        public ContactController(IContactMailer mailer, ILogger<ContactController> logger)
        {
            _mailer = mailer;
            _logger = logger;
        }

        [HttpPost("/contact")]
        [ValidateAntiForgeryToken]
        [EnableRateLimiting(RateLimitPolicy)]
        public async Task<IActionResult> Send(ContactMessage message, CancellationToken cancellationToken)
        {
            if (message.IsFromBot)
            {
                // Look successful so the bot does not retry.
                return Content("OK");
            }

            if (!ModelState.IsValid)
            {
                var error = ModelState.Values.SelectMany(v => v.Errors).First().ErrorMessage;
                return BadRequest(error);
            }

            if (!_mailer.IsAvailable)
            {
                _logger.LogWarning("Contact form used but SMTP is not configured");
                return StatusCode(503, "The contact form is not available right now. Please email me directly.");
            }

            try
            {
                await _mailer.SendAsync(message, cancellationToken);
            }
            catch (Exception exception) when (exception is not OperationCanceledException)
            {
                _logger.LogError(exception, "Sending a contact message failed");
                return StatusCode(502, "Your message could not be sent. Please try again later or email me directly.");
            }

            return Content("OK");
        }
    }
}
