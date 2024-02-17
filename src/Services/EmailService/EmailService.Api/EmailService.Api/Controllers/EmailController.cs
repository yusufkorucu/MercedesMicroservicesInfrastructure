using EmailService.Api.Core.Application.Services;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Shared.Dto;
using RabbitMQ.Shared.Events.Email;

namespace EmailService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMail([FromBody] SendEmailRequestDto sendEmailRequestDto)
        {
            var result = await _emailService.SendEmail(sendEmailRequestDto);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);

        }


        [HttpPost("SendQueue")]
        public async Task<IActionResult> SendMailWithQuee([FromBody] EmailSenderEvent emailSenderEvent)
        {
            var result = await _emailService.SendEmailWithQueue(emailSenderEvent);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);

        }
    }
}
