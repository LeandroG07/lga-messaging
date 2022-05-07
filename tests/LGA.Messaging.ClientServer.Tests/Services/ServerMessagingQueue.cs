using LGA.Messaging.Core;
using LGA.Messaging.Core.Spec;
using LGA.Messaging.Core.Spec.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGA.Messaging.ClientServer.Tests.Services
{
    internal class ServerMessagingQueue : BackgroundService
    {
        private readonly IMessagingQueue<string> _queue;

        public ServerMessagingQueue(MessagingConnection connection, ISerializer serializer)
        {
            _queue = new MessagingQueue<string>(connection, serializer, "QUEUE_TESTE");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Mensagem para enviar:");
                var message = Console.ReadLine();
                _queue.Push(message!);

                //await Task.Delay(1000);
            }
        }
    }
}
