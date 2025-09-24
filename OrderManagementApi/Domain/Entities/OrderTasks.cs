namespace OrderManagementAPI.Domain.Entities
{
    public class OrderTasks(Guid orderId, string externalTaskId)
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid OrderId { get; private set; } = orderId;
        public Order Order { get; private set; } = null!;
        public string ExternalTaskId { get; private set; } = externalTaskId;
    }
}
