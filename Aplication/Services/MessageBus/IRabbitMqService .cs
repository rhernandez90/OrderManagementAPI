namespace OrderManagementAPI.Aplication.Services.MessageBus
{
    public interface IRabbitMqService
    {
        void Publish(string queue, object message);
    }
}
