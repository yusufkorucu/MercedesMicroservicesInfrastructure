using RabbitMQ.Shared.Events.Common;

namespace RabbitMQ.Shared.Events.User
{
    public class UserCreatedEvent : IEvent
    {
        public string Email { get; set; }

    }
}
