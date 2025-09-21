using OrderManagementAPI.Aplication.DTOs;
using OrderManagementAPI.Domain.Entities;

namespace OrderManagementAPI.Aplication.Mappers
{
    public static class OrderMapper
    {
        public static OrderDto ToDto(this Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                Client = order.Client,
                Description = order.Description,
                Product = new ProductDto
                {
                    Id = order.Product.Id,
                    Sku = order.Product.Sku,
                    Name = order.Product.Name,
                    Price = order.Product.Price
                },
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                UpdatedAt = order.UpdatedAt
            };
        }
    }
}
