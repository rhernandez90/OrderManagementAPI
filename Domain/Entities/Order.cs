using OrderManagementAPI.Domain.Enums;

namespace OrderManagementAPI.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public string Client { get; private set; }
        public string Description { get; private set; }
        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Order(string client, string description, Guid productId)
        {
            Id = Guid.NewGuid();
            Client = client;
            Description = description;
            ProductId = productId;
            Status = OrderStatus.Received;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateStatus(OrderStatus newStatus)
        {
            Status = newStatus;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateDescription(string description)
        {
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
