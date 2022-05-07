using LGA.Messaging.Core.Spec.Serialization;
using Newtonsoft.Json;

namespace LGA.Messaging.Core.Serialization
{
    public class Serializer : ISerializer
    {
        public T Deserealize<T>(string data)
        {
            if (string.IsNullOrEmpty(data))
                throw new NullReferenceException(nameof(data));

            return JsonConvert.DeserializeObject<T>(data)!;
        }

        public string Serialize<T>(T model)
        {
            return JsonConvert.SerializeObject(model);
        }
    }
}
