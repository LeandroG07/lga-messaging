
namespace LGA.Messaging.Core.Spec
{
    public interface IMessagingQueue<T>
    {

        void Push(T message);

        T? Pop();

        void StartListener();

        delegate void OnReceivedHandler(T model);

        event OnReceivedHandler? OnReceived;

    }
}
