namespace RabbitMQ.Shared.Dto
{
    public class SendEmailRequestDto
    {
        public string ToMailAddress { get; set; }
        public string CcMailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
