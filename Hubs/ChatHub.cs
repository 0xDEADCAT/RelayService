using Contracts;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

using RelayService.Model;

namespace RelayService.Hubs
{
    public class ChatHub : Hub
    {
        readonly IPublishEndpoint _publishEndpoint;

        public ChatHub(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task SendMessage(Message message)
        {
            await _publishEndpoint.Publish<AddMessage>(new
            {
                CommandId = NewId.NextGuid(),
                Timestamp = DateTime.Now,
                Message = message
            });
        }
    }
}