
namespace LGA.Messaging.ClientServer.Tests
{
    public static class Configuration
    {

        public static T Factory<T>(this IConfiguration configuration, IServiceProvider provider) where T : new()
        {
            var obj = new T();
            configuration.GetSection(typeof(T).Name).Bind(obj);
            return obj;
        }

    }
}
