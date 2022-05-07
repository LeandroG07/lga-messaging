
namespace LGA.Messaging.Core.Spec
{
    public interface IQueue
    {

        void Push<T>(T message);

    }
}
