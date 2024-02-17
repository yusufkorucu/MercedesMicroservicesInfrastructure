using EmailService.Api.Core.Domain.Base;
using RabbitMQ.Shared.Dto;
using System.Net.Mail;
using System.Net;
using MassTransit;
using RabbitMQ.Shared.Events.Email;

namespace EmailService.Api.Core.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        readonly IPublishEndpoint _publishEndpoint;


        public EmailService(IConfiguration configuration, IPublishEndpoint publishEndpoint)
        {
            _configuration = configuration;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<ResultModel<bool>> SendEmail(SendEmailRequestDto sendEmailRequestDto)
        {
            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_configuration.GetSection("MailSettings")["FromMailAddress"], _configuration.GetSection("MailSettings")["SenderName"]),
                    Subject = sendEmailRequestDto.Subject,
                    Body = sendEmailRequestDto.Body,
                    IsBodyHtml = true
                };

                if (sendEmailRequestDto.ToMailAddress != null)
                {
                    var toList = sendEmailRequestDto.ToMailAddress.Split(';').Where(x => x.Contains("@")).ToList();

                    if (toList != null && toList.Any())
                    {
                        foreach (var item in toList)
                        {
                            mail.To.Add(item.Trim());
                        }
                    }

                    if (sendEmailRequestDto.CcMailAddress != null)
                    {
                        var ccList = sendEmailRequestDto.CcMailAddress.Split(';').Where(x => x.Contains("@")).ToList();

                        if (ccList != null && ccList.Any())
                        {
                            foreach (var item in ccList)
                            {
                                mail.CC.Add(item.Trim());
                            }
                        }
                    }


                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = _configuration.GetSection("MailSettings")["Host"];
                    smtp.Credentials = new NetworkCredential(
                       _configuration.GetSection("MailSettings")["FromMailAddress"],
                       _configuration.GetSection("MailSettings")["Password"]);

                    smtp.EnableSsl = true;

                    if (toList != null && toList.Any())
                    {
                      await  smtp.SendMailAsync(mail);

                        return new ResultModel<bool>() { IsSuccess = true, Result = true, Message = "Mail başarıyla gönderildi" };
                    }

                }

                return new ResultModel<bool>() { IsSuccess = false, Result = false, Message = "Mail gönderilemedi!" };

            }
            catch (Exception ex)
            {
                return new ResultModel<bool>() { Result = false, Message = ex.Message, IsSuccess = false };
            }
        }

        public async Task<ResultModel<bool>> SendEmailWithQueue(EmailSenderEvent emailSenderEvent)
        {
            try
            {
                await _publishEndpoint.Publish(emailSenderEvent);

                return new ResultModel<bool>() { IsSuccess = true, Result = true, Message = "Email Gönderilmek üzere kuyruğa başarı ile yazıldı" };
            }
            catch (Exception ex)
            {
                return new ResultModel<bool>() { IsSuccess = false, Result = false, Message = $"Email kuyruğa yazılırken bir hata oluştu{ex.Message}" };

               
            }
        }
    }
}
