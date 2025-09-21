using Microsoft.EntityFrameworkCore;
using OrderManagementAPI.Aplication.DTOs;
using OrderManagementAPI.Aplication.DTOs.Orders;
using OrderManagementAPI.Aplication.DTOs.Products;
using OrderManagementAPI.Aplication.Exceptions;
using OrderManagementAPI.Aplication.Mappers;
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

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto dto)
        {
            var product = await _context.Products.FindAsync(dto.ProductId)
                          ?? throw new BusinessException("Product not found");

            var order = new Order(dto.Client, dto.Description, product.Id);

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order.ToDto();
        }

        public async Task<OrderDto?> GetOrderAsync(Guid id)
        {
            var order = await _context.Orders.Include(o => o.Product)
                        .FirstOrDefaultAsync(o => o.Id == id);

            return order?.ToDto() ?? null;
        }

        public async Task<OrderDto> UpdateOrderAsync(Guid id, CreateOrderDto dto)
        {
            var order = await _context.Orders.Include(o => o.Product)
                        .FirstOrDefaultAsync(o => o.Id == id)
                        ?? throw new BusinessException("Order not found");

            // ejemplo simple, puedes separar métodos de actualización
            order.UpdateDescription(dto.Description);
            await _context.SaveChangesAsync();
            return order.ToDto();
        }

        public async Task DeleteOrderAsync(Guid id)
        {
            var order = await _context.Orders.Include(o => o.Product)
                        .FirstOrDefaultAsync(o => o.Id == id)
                        ?? throw new BusinessException("Order not found");

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<OrderDto> ChangeStatusAsync(Guid id, ChangeOrderStatusDto dto)
        {
            var order = await _context.Orders.Include(o => o.Product)
                        .FirstOrDefaultAsync(o => o.Id == id)
                        ?? throw new BusinessException("Order not found");

            // validación de transición
            if (!IsValidTransition(order.Status, dto.NewStatus))
                throw new BusinessException($"Invalid transition from {order.Status} to {dto.NewStatus}");

            order.UpdateStatus(dto.NewStatus);
            await _context.SaveChangesAsync();
            return order.ToDto();
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

        public async Task<PagedResult<OrderDto>> SearchOrdersAsync(OrderFilterDto filter)
        {
            var query = _context.Orders.Include(o => o.Product).AsQueryable();

            if (!string.IsNullOrEmpty(filter.Client))
                query = query.Where(o => o.Client.Contains(filter.Client));

            if (!string.IsNullOrEmpty(filter.Description))
                query = query.Where(o => o.Description.Contains(filter.Description));

            if (filter.Status.HasValue)
                query = query.Where(o => o.Status == filter.Status);

            var items = await query
              .Skip((filter.Page - 1) * filter.PageSize)
              .Take(filter.PageSize)
              .Select(x => x.ToDto())
              .ToListAsync();

            var totalCount = await query.CountAsync();

            return new PagedResult<OrderDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = filter.Page,
                PageSize = filter.PageSize
            };
        }
    }
}
