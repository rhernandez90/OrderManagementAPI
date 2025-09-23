using OrderManagementAPI.Aplication.DTOs.Products;
using OrderManagementAPI.Domain.Enums;

namespace OrderManagementAPI.Aplication.DTOs.Orders
{
    public class OrderFilterDto
    {
        public string? Client { get; set; }
        public string? Description { get; set; }
        public OrderStatus? Status { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
