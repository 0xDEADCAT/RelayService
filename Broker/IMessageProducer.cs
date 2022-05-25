using RelayService.Model;

namespace RelayService.Broker
{
    public interface IMessageProducer
    {
        void SendMessage<T>(Message msg);
    }
}