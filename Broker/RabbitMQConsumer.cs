using System.Text;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using RelayService.Hubs;

namespace RelayService.Broker
{
    public class RabbitMQConsumer : IMessageConsumer
    {
        protected readonly ConnectionFactory _factory;
        protected readonly IConnection _connection;
        protected readonly IModel _channel;
        protected readonly IServiceProvider _serviceProvider;

        public RabbitMQConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _factory = new ConnectionFactory() { HostName = "rabbitmq" };
            const int retries = 3;
            for (int i = 1; i <= retries; i++)
            {
                try
                {
                    _connection = _factory.CreateConnection();
                    break;
                } catch (BrokerUnreachableException)
                {
                    Console.WriteLine($"[{i}/{retries}] Could not connect to RabbitMQ. Retrying...");
                }
            }
            _channel = _connection.CreateModel();

            System.Console.WriteLine("\nConnected to factory!\n");
        }

        public void ReceiveMessages()
        {
            // Declare a RabbitMQ Queue
            _channel.QueueDeclare(queue: "chat2", durable: false, exclusive: false, autoDelete: false);
 
            var consumer = new EventingBasicConsumer(_channel);
 
            // When we receive a message from SignalR
            consumer.Received += delegate (object model, BasicDeliverEventArgs ea) {
                // Get the ChatHub from SignalR (using DI)
                var chatHub = (IHubContext<ChatHub>)_serviceProvider.GetService(typeof(IHubContext<ChatHub>));

                // Get the message from RabbitMQ
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var json = JsonConvert.DeserializeObject(message);
                
                System.Console.WriteLine(json);

                // Send message to all users in SignalR
                chatHub.Clients.All.SendAsync("messageReceived", "poopster", json);
 
            };
 
            // Consume a RabbitMQ Queue
            _channel.BasicConsume(queue: "chat2", autoAck: true, consumer: consumer);

        }
    }
}