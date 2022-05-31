namespace ChatService.Consumers
{
    using Microsoft.AspNetCore.SignalR;
    using System.Threading.Tasks;
    using MassTransit;
    using Contracts;
    using RelayService.Hubs;

    public class RelayServiceConsumer :
        IConsumer<MessageAdded>, IConsumer<MessageDeleted>
    {
        protected readonly IServiceProvider _serviceProvider;

        public RelayServiceConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Consume(ConsumeContext<MessageAdded> context)
        {
            var chatHub = (IHubContext<ChatHub>)_serviceProvider.GetService(typeof(IHubContext<ChatHub>));

            await chatHub.Clients.All.SendAsync("messageReceived", context.Message.Message);
        }

        public async Task Consume(ConsumeContext<MessageDeleted> context)
        {
            var chatHub = (IHubContext<ChatHub>)_serviceProvider.GetService(typeof(IHubContext<ChatHub>));

            await chatHub.Clients.All.SendAsync("messagusDeletus", context.Message.MessId);
        }
    }
}