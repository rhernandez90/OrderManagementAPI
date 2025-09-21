namespace OrderManagementAPI.Aplication.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
