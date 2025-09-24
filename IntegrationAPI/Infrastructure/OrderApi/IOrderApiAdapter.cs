namespace IntegrationAPI.Infrastructure.OrderApi
{
    public interface IOrderApiAdapter
    {
        Task AssignTaskToOrderAsync(Guid orderId, string taskId);
    }
}
