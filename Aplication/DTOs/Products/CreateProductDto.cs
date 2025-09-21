using OrderManagementAPI.Domain.Enums;

namespace OrderManagementAPI.Aplication.DTOs.Products
{
    public class CreateProductDto
    {
        public required string Sku { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public ProductType Type { get; set; }

        // Opcionales
        public string? Size { get; set; }
        public string? Color { get; set; }
        public int? CapacityMl { get; set; }
        public int? HeightCm { get; set; }
        public int? WidthCm { get; set; }
        public string? Paper { get; set; }
    }
}
