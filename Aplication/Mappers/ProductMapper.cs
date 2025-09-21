using OrderManagementAPI.Aplication.DTOs.Products;
using OrderManagementAPI.Domain.Entities;

namespace OrderManagementAPI.Aplication.Mappers
{
    public static class ProductMapper
    {

        public static ProductDto ToDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Sku = product.Sku,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Type = product.Type,
                Size = product.Size,
                Color = product.Color,
                CapacityMl = product.CapacityMl,
                HeightCm = product.HeightCm,
                WidthCm = product.WidthCm,
                Paper = product.Paper
            };
        }
    }

}
