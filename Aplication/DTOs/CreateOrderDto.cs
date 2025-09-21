namespace OrderManagementAPI.Aplication.DTOs
{
    public class CreateOrderDto
    {
        public string Client { get; set; }
        public string Description { get; set; }
        public Guid ProductId { get; set; }
    }
}
