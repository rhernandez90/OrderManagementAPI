using IntegrationAPI.Application.DTOs;
using IntegrationAPI.Application.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace IntegrationAPI.Application.Consumer
{
    public class OrderCreatedConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _config;
        private IConnection _connection;
        private IModel _channel;
        private readonly string _queueName;

        public OrderCreatedConsumer(IServiceProvider serviceProvider, IConfiguration config)
        {
            _serviceProvider = serviceProvider;
            _config = config;

            var factory = new ConnectionFactory()
            {
                HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost",
                Port = int.Parse(Environment.GetEnvironmentVariable("RABBITMQ_PORT") ?? "5672"),
                UserName = Environment.GetEnvironmentVariable("RABBITMQ_USER") ?? "guest",
                Password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "guest"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _queueName = "orders";

            _channel.QueueDeclare(queue: _queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                dynamic orderEvent = JsonConvert.DeserializeObject(message);

                using var scope = _serviceProvider.CreateScope();
                var trelloService = scope.ServiceProvider.GetRequiredService<ITrelloService>();

                string eventName = orderEvent.Event?.ToString();
                OrderDto order = new OrderDto
                {
                    OrderId = Guid.Parse(orderEvent.OrderId.ToString()),
                    Client = orderEvent.Client.ToString(),
                    Status = orderEvent.Status.ToString(),
                    Product = orderEvent.Product,
                    ExternalTaskId = orderEvent?.ExternalTaskId ?? ""
                };

                if (eventName == "OrderCreated")
                {
                    await trelloService.CreateCardAsync(order);
                }
                else if (eventName == "OrderUpdated")
                {
                    if (!string.IsNullOrEmpty(order.ExternalTaskId))
                        await trelloService.UpdateCardStatusAsync(order);
                }
            };

            _channel.BasicConsume(queue: _queueName,
                                 autoAck: true,
                                 consumer: consumer);

            return Task.CompletedTask;
        }

    }
}
