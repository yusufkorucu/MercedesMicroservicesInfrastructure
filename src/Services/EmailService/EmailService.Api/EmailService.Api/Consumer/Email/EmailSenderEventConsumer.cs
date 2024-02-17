using EmailService.Api.Core.Application.Services;
using MassTransit;
using RabbitMQ.Shared.Dto;
using RabbitMQ.Shared.Events.Email;

namespace EmailService.Api.Consumer.Email
{
    public class EmailSenderEventConsumer : IConsumer<EmailSenderEvent>
    {
        private readonly IEmailService _emailService;

        public EmailSenderEventConsumer(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Consume(ConsumeContext<EmailSenderEvent> context)
        {
            var mailDto = new SendEmailRequestDto()
            {
                Body = context.Message.Body,
                Subject = context.Message.Subject,
                CcMailAddress = context.Message.CcMailAddress,
                ToMailAddress = context.Message.ToMailAddress,
            };

            await _emailService.SendEmail(mailDto);
        }
    }
}
