using Microsoft.AspNetCore.Mvc;
using OrderManagementAPI.Aplication.DTOs;
using OrderManagementAPI.Aplication.Mappers;
using OrderManagementAPI.Aplication.Services;
using OrderManagementAPI.Controllers.DTOs;
using OrderManagementAPI.Domain.Enums;

namespace OrderManagementAPI.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
        {
            var order = await _orderService.CreateOrderAsync(dto);
            var response = new ApiResponseDto<OrderDto>(order.ToDto(), "Order created successfully");
            return CreatedAtAction(nameof(Get), new { id = order.Id }, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var order = await _orderService.GetOrderAsync(id);
            if (order is null)
                return NotFound(new ApiResponseDto<OrderDto>(new List<string> { "Order not found" }));

            var response = new ApiResponseDto<OrderDto>(order.ToDto(), "Order retrieved successfully");
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateOrderDto dto)
        {
            var order = await _orderService.UpdateOrderAsync(id, dto);
            var response = new ApiResponseDto<OrderDto>(order.ToDto(), "Order updated successfully");
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _orderService.DeleteOrderAsync(id);
            var response = new ApiResponseDto<object>(null, "Order deleted successfully");
            return Ok(response);
        }

        [HttpPost("{id}/change-state")]
        public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] ChangeOrderStatusDto dto)
        {
            var order = await _orderService.ChangeStatusAsync(id, dto);
            var response = new ApiResponseDto<OrderDto>(order.ToDto(), $"Order status changed to {order.Status}");
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string? client, [FromQuery] OrderStatus? status,
            [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var orders = await _orderService.SearchOrdersAsync(client, status, page, size);

            var orderDtos = orders.Select(o => o.ToDto());

            var response = new ApiResponseDto<IEnumerable<OrderDto>>(orderDtos, "Orders retrieved successfully");
            return Ok(response);
        }
    }
}
