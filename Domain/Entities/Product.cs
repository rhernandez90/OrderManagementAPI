using OrderManagementAPI.Domain.Enums;

namespace OrderManagementAPI.Domain.Entities
{
    public class Product
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Sku { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public ProductType Type { get; set; }

        // Optionals fields
        public string? Size { get; set; }
        public string? Color { get; set; }
        public int? CapacityMl { get; set; }
        public int? HeightCm { get; set; }
        public int? WidthCm { get; set; }
        public string? Paper { get; set; }


        public Product() { }

        public Product(string sku, string name, decimal price, ProductType type)
        {
            Sku = sku;
            Name = name;
            Price = price;
            Type = type;
        }
        


        
    }
}
