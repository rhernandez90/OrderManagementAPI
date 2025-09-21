using OrderManagementAPI.Domain.Enums;

namespace OrderManagementAPI.Aplication.DTOs.Orders
{
    public class ChangeOrderStatusDto
    {
        public OrderStatus NewStatus { get; set; }
        public string? Comment { get; set; }
    }
}
