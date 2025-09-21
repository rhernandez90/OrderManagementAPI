using OrderManagementAPI.Aplication.DTOs;
using OrderManagementAPI.Aplication.DTOs.Orders;
using OrderManagementAPI.Aplication.DTOs.Products;
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
                    Price = order.Product.Price,
                    Type = order.Product.Type,
                    HeightCm = order.Product.HeightCm,
                    WidthCm = order.Product.WidthCm,
                    CapacityMl = order.Product.CapacityMl,
                    Color = order.Product.Color,
                    Description = order.Product.Description
                },
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                UpdatedAt = order.UpdatedAt
            };
        }
    }
}
