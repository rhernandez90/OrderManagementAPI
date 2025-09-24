namespace OrderManagementAPI.Aplication.Services.OrderTask
{
    public interface IOrderTaskService
    {
        Task<Guid> CreateOrderTaskAsync(Guid OrderId, string TaskId);
    }
}
