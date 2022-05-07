using LGA.Messaging.Core.Spec.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
