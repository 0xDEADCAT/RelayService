namespace RelayService.Broker
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T user, T content);
    }
}