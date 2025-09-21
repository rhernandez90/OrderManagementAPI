using OrderManagementAPI.Domain.Enums;

namespace OrderManagementAPI.Aplication.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string Client { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ProductDto Product { get; set; } = null!;
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
