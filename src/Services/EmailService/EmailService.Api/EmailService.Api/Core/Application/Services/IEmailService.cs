using EmailService.Api.Core.Domain.Base;
using RabbitMQ.Shared.Dto;
using RabbitMQ.Shared.Events.Email;

namespace EmailService.Api.Core.Application.Services
{
    public interface IEmailService
    {
        Task<ResultModel<bool>> SendEmail(SendEmailRequestDto sendEmailRequestDto);
        Task<ResultModel<bool>> SendEmailWithQueue(EmailSenderEvent emailSenderEvent);
    }
}
