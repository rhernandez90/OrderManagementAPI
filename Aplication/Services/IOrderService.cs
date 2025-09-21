using OrderManagementAPI.Aplication.DTOs;
using OrderManagementAPI.Domain.Entities;
using OrderManagementAPI.Domain.Enums;

namespace OrderManagementAPI.Aplication.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(CreateOrderDto dto);
        Task<Order?> GetOrderAsync(Guid id);
        Task<Order> UpdateOrderAsync(Guid id, CreateOrderDto dto);
        Task DeleteOrderAsync(Guid id);
        Task<Order> ChangeStatusAsync(Guid id, ChangeOrderStatusDto dto);
        Task<IEnumerable<Order>> SearchOrdersAsync(string? client, OrderStatus? status, int page, int size);
    }
}
