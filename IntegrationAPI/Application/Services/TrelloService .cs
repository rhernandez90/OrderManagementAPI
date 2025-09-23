using IntegrationAPI.Application.DTOs;

namespace IntegrationAPI.Application.Services
{
    public class TrelloService : ITrelloService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public TrelloService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task CreateOrUpdateCardAsync(OrderDto order)
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
        }
    }
}
