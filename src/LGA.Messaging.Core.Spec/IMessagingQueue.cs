
namespace LGA.Messaging.Core.Abstractions
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
