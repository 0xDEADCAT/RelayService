namespace ChatService.Consumers
{
    using MassTransit;

    public class RelayServiceConsumerDefinition :
        ConsumerDefinition<RelayServiceConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<RelayServiceConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        }
    }
}