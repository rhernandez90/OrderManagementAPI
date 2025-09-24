using IntegrationAPI.Application.DTOs;
using IntegrationAPI.Infrastructure.OrderApi;
using System.Text.Json;

namespace IntegrationAPI.Application.Services
{
    public class TrelloService : ITrelloService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly IOrderApiAdapter _orderApiAdapter;
        public TrelloService(HttpClient httpClient, IConfiguration config, IOrderApiAdapter orderApiAdapter)
        {
            _httpClient = httpClient;
            _config = config;
            _orderApiAdapter = orderApiAdapter;
        }

        public async Task CreateCardAsync(OrderDto order)
        {
            var ApiKey = Environment.GetEnvironmentVariable("TRELLO_APIKEY") ?? "";
            var Token = Environment.GetEnvironmentVariable("TRELLO_TOKEN") ?? "";
            var BoradId = Environment.GetEnvironmentVariable("TRELLO_BOARDID") ?? "";
            var ListId = Environment.GetEnvironmentVariable("TRELLO_LISTID") ?? "";


            var url = $"https://api.trello.com/1/cards?idList={ListId}&key={ApiKey}&token={Token}";

            var content = new Dictionary<string, string>
            {
                {"name", $"Order {order.OrderId} - {order.Client}"},
                {"desc", @$"Status: {order.Status}
                            Product Detail: {order.Product}"},

            };

            var response = await _httpClient.PostAsync(url, new FormUrlEncodedContent(content));
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var card = JsonSerializer.Deserialize<TrelloCardResponse>(json);

            if (card != null)
                await _orderApiAdapter.AssignTaskToOrderAsync(order.OrderId, card.id);
        }


        public async Task UpdateCardStatusAsync(OrderDto order)
        {
            var ApiKey = Environment.GetEnvironmentVariable("TRELLO_APIKEY") ?? "";
            var Token = Environment.GetEnvironmentVariable("TRELLO_TOKEN") ?? "";

            var url = $"https://api.trello.com/1/cards/{order.ExternalTaskId}?key={ApiKey}&token={Token}";

            var content = new Dictionary<string, string>
            {
                {"desc", @$"Status: {order.Status} 
                            Product Detail: {order.Product}"}
            };

            var response = await _httpClient.PutAsync(url, new FormUrlEncodedContent(content));
            response.EnsureSuccessStatusCode();
        }

    }
}
