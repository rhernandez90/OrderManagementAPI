namespace OrderManagementAPI.Domain.Entities
{
    public class Product(string sku, string name, decimal price)
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Sku { get; private set; } = sku;
        public string Name { get; private set; } = name;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; } = price;

        // Optionals fields
        public string? Size { get; private set; }
        public string? Color { get; private set; }
        public int? CapacityMl { get; private set; }
        public int? HeightCm { get; private set; }
        public int? WidthCm { get; private set; }
        public string? Paper { get; private set; }
    }
}
