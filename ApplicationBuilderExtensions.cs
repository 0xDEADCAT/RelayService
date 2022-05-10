using RelayService.Broker;

namespace RelayService
{
    public static class ApplicationBuilderExtentions
    {
        public static RabbitMQConsumer Listener { get; set; }

        public static IApplicationBuilder EnableBrokerListener(this IApplicationBuilder app)
        {
            Listener = app.ApplicationServices.GetService<RabbitMQConsumer>();

            var life = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            life.ApplicationStarted.Register(OnStarted);

            return app;
        }

        private static void OnStarted()
        {
            Listener.ReceiveMessages();
        }
    }
}