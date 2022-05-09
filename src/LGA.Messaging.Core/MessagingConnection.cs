using LGA.Messaging.Core.Abstractions.Configuration;
using RabbitMQ.Client;

namespace LGA.Messaging.Core
{
    public class MessagingConnection
    {
        private readonly ConnectionFactory _connectionFactory;

        public MessagingConnection(MessagingOption option)
        {
            _connectionFactory = new ConnectionFactory()
            {
                HostName = option.HostName
            };
        }

        public IModel Channel
        {
            get
            {
                var connection = _connectionFactory.CreateConnection();
                return connection.CreateModel();
            }
        }

    }
}
