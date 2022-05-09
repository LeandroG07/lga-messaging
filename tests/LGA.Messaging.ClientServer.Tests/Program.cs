using LGA.Messaging.ClientServer.Tests;
using LGA.Messaging.ClientServer.Tests.Services;
using LGA.Messaging.Core;
using LGA.Messaging.Core.Serialization;
using LGA.Messaging.Core.Abstractions.Configuration;
using LGA.Messaging.Core.Abstractions.Serialization;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        services.AddSingleton(configuration.Factory<MessagingOption>);

        services.AddSingleton<ISerializer, Serializer>();
        services.AddSingleton<MessagingConnection>();

        services.AddTransient<ClientMessagingQueue>();

        services.AddHostedService<ClientMessagingQueue>();
        services.AddHostedService<ClientMessagingQueue2>();
        services.AddHostedService<ServerMessagingQueue>();
    })
    .Build();

await host.RunAsync();