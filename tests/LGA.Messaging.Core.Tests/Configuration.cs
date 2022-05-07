using LGA.Messaging.Core.Serialization;
using LGA.Messaging.Core.Spec;
using LGA.Messaging.Core.Spec.Configuration;
using LGA.Messaging.Core.Spec.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGA.Messaging.Core.Tests
{
    public static class Configuration
    {

        public static IServiceCollection Configure(this IServiceCollection services)
        {            
            var configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();            
            
            services.AddSingleton(configuration.Factory<MessagingOption>);
            
            var provider = services.BuildServiceProvider();

            var option = provider.GetRequiredService<MessagingOption>();
            services.AddScoped(_ => new MessagingConnection(option!));

            services.AddScoped<ISerializer, Serializer>();

            return services;
        }


        private static T Factory<T>(this IConfiguration configuration, IServiceProvider service) where T : new()
        {
            T obj = new();
            configuration.GetSection(typeof(T).Name).Bind(obj);
            return obj;
        }

    }
}
