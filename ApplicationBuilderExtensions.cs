using RelayService.Broker;

namespace RelayService
{
    public static class ApplicationBuilderExtentions
    {
        public static IMessageConsumer Listener { get; set; }

        public static IApplicationBuilder EnableBrokerListener(this IApplicationBuilder app)
        {
            Listener = app.ApplicationServices.GetService<IMessageConsumer>();

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