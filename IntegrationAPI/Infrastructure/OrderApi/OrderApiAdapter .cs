namespace IntegrationAPI.Infrastructure.OrderApi
{
    public class OrderApiAdapter : IOrderApiAdapter
    {
        private readonly HttpClient _httpClient;

        public OrderApiAdapter(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AssignTaskToOrderAsync(Guid orderId, string taskId)
        {
            // Asumiendo que la base URL de OrderManagementAPI ya está configurada en HttpClient
            var url = $"orders/{orderId}/assign-task/{taskId}";
            var response = await _httpClient.PostAsync(url, null);
            response.EnsureSuccessStatusCode();
        }
    }
}
