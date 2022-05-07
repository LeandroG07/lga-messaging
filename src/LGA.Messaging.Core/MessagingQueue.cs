using LGA.Messaging.Core.Spec;
using LGA.Messaging.Core.Spec.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace LGA.Messaging.Core
{
    public class MessagingQueue<T> : IMessagingQueue<T>
    {
        private readonly MessagingConnection _connection;
        private readonly ISerializer _serializer;
        private readonly string _queueName;
        private IModel? _channel;

        public MessagingQueue(MessagingConnection connection, ISerializer serializer, string name)
        {
            _connection = connection;
            _serializer = serializer;
            _queueName = name;
        }

        private IModel Channel
        {
            get
            {
                if (_channel == null)
                    _channel = ConfigureChannel();

                return _channel;
            }

        }

        public void Push(T message)
        {
            var data = _serializer.Serialize(message);
            var bytes = Encoding.ASCII.GetBytes(data);
            Channel.BasicPublish(string.Empty, _queueName, null, bytes);
        }

        public T? Pop()
        {
            var message = Channel.BasicGet(_queueName, true);
            if (message == null)
                return default;

            var bytes = message.Body.ToArray();
            var model = Encoding.ASCII.GetString(bytes);
            return _serializer.Deserealize<T>(model);
        }

        public void StartListener()
        {
            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += ReceiveMessage;
            Channel.BasicConsume(_queueName, true, consumer);
        }

        private void ReceiveMessage(object? sender, BasicDeliverEventArgs e)
        {
            var bytes = e.Body.ToArray();
            var message = Encoding.ASCII.GetString(bytes);
            var model = _serializer.Deserealize<T>(message);
            OnReceived?.Invoke(model);
        }

        public event IMessagingQueue<T>.OnReceivedHandler? OnReceived;

        private IModel ConfigureChannel()
        {
            _connection.Channel.QueueDeclare(_queueName, false, false, false, null);
            return _connection.Channel;
        }
    }

}
