namespace OrderManagementAPI.Aplication.DTOs.Products
{
    public class ProductFilterDto
    {
        public string? Name { get; set; }
        public string? SKU { get; set; }
        public string? Description { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
