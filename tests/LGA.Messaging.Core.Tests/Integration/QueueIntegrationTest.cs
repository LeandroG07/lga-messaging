using LGA.Messaging.Core.Spec.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LGA.Messaging.Core.Tests.Integration
{
    public class QueueIntegrationTest
    {
        private readonly ServiceProvider _provider;

        public QueueIntegrationTest()
        {
            _provider = new ServiceCollection().Configure().BuildServiceProvider();
        }

        [Theory]
        [InlineData("QUEUE_TEST", "send test")]
        [InlineData("QUEUE_TEST", "send test 2")]
        public void ShouldPushMessageQueue(string queuName, string message)
        {            
            var queue = new MessagingQueue<string>(_provider.GetService<MessagingConnection>()!, _provider.GetService<ISerializer>()!, queuName);
            
            queue!.Push(message);

            Assert.True(true);
        }

        [Theory]
        [InlineData("QUEUE_TEST")]
        public void ShouldPopMessageQueue(string queuName)
        {
            var queue = new MessagingQueue<string>(_provider.GetService<MessagingConnection>()!, _provider.GetService<ISerializer>()!, queuName);

            var value = queue!.Pop();

            Assert.True(true);
        }

        [Theory]
        [InlineData("QUEUE_TEST")]
        public void ShouldStartListener(string queuName)
        {
            var queue = new MessagingQueue<string>(_provider.GetService<MessagingConnection>()!, _provider.GetService<ISerializer>()!, queuName);

            queue.StartListener();

            queue.OnReceived += ReceiveMessage;

            Assert.True(true);
        }

        private void ReceiveMessage(string model)
        {
            var message = model;
        }
    }
}
