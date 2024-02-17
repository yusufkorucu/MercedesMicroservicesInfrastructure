using RabbitMQ.Shared.Events.Common;

namespace RabbitMQ.Shared.Events.Email
{
    public class EmailSenderEvent:IEvent
    {
        public string ToMailAddress { get; set; }
        public string CcMailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
