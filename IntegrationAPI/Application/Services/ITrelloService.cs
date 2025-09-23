using IntegrationAPI.Application.DTOs;

namespace IntegrationAPI.Application.Services
{
    public interface ITrelloService
    {
        Task CreateOrUpdateCardAsync(OrderDto order);
    }
}
