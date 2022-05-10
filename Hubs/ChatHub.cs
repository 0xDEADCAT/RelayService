using Microsoft.AspNetCore.SignalR;

using RelayService.Broker;

namespace RelayService.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageProducer _messageProducer;

        public ChatHub(IMessageProducer messageProducer)
        {

            _messageProducer = messageProducer;
        }

        public void SendMessage(string user, string message)
        {
            System.Console.WriteLine("\nPOOP\n");
            _messageProducer.SendMessage(message);
        }
    }
}