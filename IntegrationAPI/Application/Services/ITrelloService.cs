using IntegrationAPI.Application.DTOs;

namespace IntegrationAPI.Application.Services
{
    public interface ITrelloService
    {
        Task CreateCardAsync(OrderDto order);
        Task UpdateCardStatusAsync(OrderDto order);
    }
}
