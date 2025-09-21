using Microsoft.EntityFrameworkCore;
using OrderManagementAPI.Aplication.DTOs;
using OrderManagementAPI.Aplication.Exceptions;
using OrderManagementAPI.Domain.Entities;
using OrderManagementAPI.Domain.Enums;
using OrderManagementAPI.Infrastructure.Percistence;

namespace OrderManagementAPI.Aplication.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(CreateOrderDto dto)
        {
            var product = await _context.Products.FindAsync(dto.ProductId)
                          ?? throw new BusinessException("Product not found");

            var order = new Order(dto.Client, dto.Description, product.Id);

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> GetOrderAsync(Guid id)
        {
            return await _context.Orders.Include(o => o.Product)
                        .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order> UpdateOrderAsync(Guid id, CreateOrderDto dto)
        {
            var order = await GetOrderAsync(id)
                        ?? throw new BusinessException("Order not found");

            // ejemplo simple, puedes separar métodos de actualización
            order.UpdateDescription(dto.Description);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task DeleteOrderAsync(Guid id)
        {
            var order = await GetOrderAsync(id)
                        ?? throw new BusinessException("Order not found");

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> ChangeStatusAsync(Guid id, ChangeOrderStatusDto dto)
        {
            var order = await GetOrderAsync(id)
                        ?? throw new BusinessException("Order not found");

            // validación de transición
            if (!IsValidTransition(order.Status, dto.NewStatus))
                throw new BusinessException($"Invalid transition from {order.Status} to {dto.NewStatus}");

            order.UpdateStatus(dto.NewStatus);
            await _context.SaveChangesAsync();
            return order;
        }

        private bool IsValidTransition(OrderStatus current, OrderStatus next)
        {
            return current switch
            {
                OrderStatus.Received => next == OrderStatus.InReview,
                OrderStatus.InReview => next == OrderStatus.Approved,
                OrderStatus.Approved => next == OrderStatus.InProduction,
                OrderStatus.InProduction => next == OrderStatus.Delivered,
                _ => false
            };
        }

        public async Task<IEnumerable<Order>> SearchOrdersAsync(string? client, OrderStatus? status, int page, int size)
        {
            var query = _context.Orders.Include(o => o.Product).AsQueryable();

            if (!string.IsNullOrEmpty(client))
                query = query.Where(o => o.Client.Contains(client));

            if (status.HasValue)
                query = query.Where(o => o.Status == status);

            return await query.Skip((page - 1) * size).Take(size).ToListAsync();
        }
    }
}
