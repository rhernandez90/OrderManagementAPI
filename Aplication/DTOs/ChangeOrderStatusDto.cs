using OrderManagementAPI.Domain.Enums;

namespace OrderManagementAPI.Aplication.DTOs
{
    public class ChangeOrderStatusDto
    {
        public OrderStatus NewStatus { get; set; }
        public string? Comment { get; set; }
    }
}
