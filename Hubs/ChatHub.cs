using Microsoft.AspNetCore.SignalR;

using RelayService.Broker;
using RelayService.Model;

namespace RelayService.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageProducer _messageProducer;

        public ChatHub(IMessageProducer messageProducer)
        {

            _messageProducer = messageProducer;
        }

        public void SendMessage(Message message)
        {
            _messageProducer.SendMessage<Message>(message);

        }
    }
}