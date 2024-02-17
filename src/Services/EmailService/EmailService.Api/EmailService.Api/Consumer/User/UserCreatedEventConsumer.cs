using EmailService.Api.Core.Application.Services;
using MassTransit;
using RabbitMQ.Shared.Dto;
using RabbitMQ.Shared.Events.User;

namespace EmailService.Api.Consumer.User
{
    public class UserCreatedEventConsumer : IConsumer<UserCreatedEvent>
    {
        private IEmailService _emailService;

        public UserCreatedEventConsumer(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            var dto = new SendEmailRequestDto()
            {
                ToMailAddress = context.Message.Email,
                Body = $"Hoş Geldin Sayın {context.Message.Email}",
                Subject = "Hoş Geldin Maili"
            };
            await _emailService.SendEmail(dto);
        }
    }
}
