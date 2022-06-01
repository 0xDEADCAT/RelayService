using Contracts;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

using RelayService.Models;

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

        public async Task DeleteMessage(String messageId)
        {
            await _publishEndpoint.Publish<DeleteMessage>(new
            {
                CommandId = NewId.NextGuid(),
                Timestamp = DateTime.Now,
                MessId = messageId
            });
        }

        public async Task Typing() => await Clients.All.SendAsync("SomeoneTyping");
    }
}