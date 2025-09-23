namespace IntegrationAPI.Application.DTOs
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public string Client { get; set; }
        public string Status { get; set; }
        public object Product { get; set; }
    }
}
