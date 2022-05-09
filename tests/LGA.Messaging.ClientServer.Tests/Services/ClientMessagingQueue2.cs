using LGA.Messaging.Core;
using LGA.Messaging.Core.Abstractions;
using LGA.Messaging.Core.Abstractions.Serialization;

namespace LGA.Messaging.ClientServer.Tests.Services
{
    public class ClientMessagingQueue2 : BackgroundService
    {
        private readonly IMessagingQueue<string> _queue;
        private readonly int _consoleNumber;

        public ClientMessagingQueue2(MessagingConnection connection, ISerializer serializer)
        {
            _queue = new MessagingQueue<string>(connection, serializer, "QUEUE_TESTE");
            _consoleNumber = new Random().Next(1000, 9999);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _queue.StartListener();
            _queue.OnReceived += ReceiveMessage;

            Console.WriteLine($"client on [{_consoleNumber}]");
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000);
            }
        }

        private void ReceiveMessage(string model)
        {
            Console.WriteLine($"mensagem recebida [{_consoleNumber}]: {model}");
        }
    }
}
