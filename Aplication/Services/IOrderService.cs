using OrderManagementAPI.Aplication.DTOs;
using OrderManagementAPI.Aplication.DTOs.Orders;
using OrderManagementAPI.Domain.Entities;
using OrderManagementAPI.Domain.Enums;

namespace OrderManagementAPI.Aplication.Services
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(CreateOrderDto dto);
        Task<OrderDto?> GetOrderAsync(Guid id);
        Task<OrderDto> UpdateOrderAsync(Guid id, CreateOrderDto dto);
        Task DeleteOrderAsync(Guid id);
        Task<OrderDto> ChangeStatusAsync(Guid id, ChangeOrderStatusDto dto);
        Task<PagedResult<OrderDto>> SearchOrdersAsync(OrderFilterDto filter);
    }
}
