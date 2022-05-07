using LGA.Messaging.Core.Spec;
using LGA.Messaging.Core.Spec.Serialization;
using RabbitMQ.Client;
using System.Text;

namespace LGA.Messaging.Core
{
    public class Queue : IQueue
    {
        private readonly MessagingConnection _connection;
        private readonly ISerializer _serializer;
        private readonly string _name;
        private IModel? _channel;

        public Queue(MessagingConnection connection, ISerializer serializer, string name)
        {
            _connection = connection;
            _serializer = serializer;
            _name = name;
        }

        public IModel Channel 
        {
            get
            {
                if (_channel == null)
                    _channel = ConfigureChannel();

                return _channel;
            }
            
        }

        public void Push<T>(T message)
        {
            var data = _serializer.Serialize(message);
            var bytes = Encoding.ASCII.GetBytes(data);
            Channel.BasicPublish(string.Empty, _name, null, bytes);
        }

        private IModel ConfigureChannel()
        {
            _connection.Channel.QueueDeclare(_name, false, false, false, null);
            return _connection.Channel;
        }
    }
}
