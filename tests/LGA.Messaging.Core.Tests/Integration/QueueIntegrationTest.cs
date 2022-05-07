using LGA.Messaging.Core.Spec;
using LGA.Messaging.Core.Spec.Configuration;
using LGA.Messaging.Core.Spec.Serialization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LGA.Messaging.Core.Tests.Integration
{
    public class QueueIntegrationTest
    {


        [Fact]
        private void Should()
        {
            var provider = new ServiceCollection().Configure().BuildServiceProvider();

            var x = provider.GetService<MessagingOption>();

            var queue = new Queue(provider.GetService<MessagingConnection>(), provider.GetService<ISerializer>(), "QUEUE_TEST");
            queue!.Push<dynamic>(new { field = "test1" });
            
        }

    }
}
