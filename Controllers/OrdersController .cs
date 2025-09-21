using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderManagementAPI.Aplication.DTOs.Orders;
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
            var response = new ApiResponseDto<OrderDto>(order, "Order created successfully");
            return CreatedAtAction(nameof(Get), new { id = order.Id }, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var order = await _orderService.GetOrderAsync(id);
            if (order is null)
                return NotFound(new ApiResponseDto<OrderDto>(new List<string> { "Order not found" }));

            var response = new ApiResponseDto<OrderDto>(order, "Order retrieved successfully");
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateOrderDto dto)
        {
            var order = await _orderService.UpdateOrderAsync(id, dto);
            var response = new ApiResponseDto<OrderDto>(order, "Order updated successfully");
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _orderService.DeleteOrderAsync(id);
            var response = new ApiResponseDto<object>(data: null, "Order deleted successfully");
            return Ok(response);
        }

        [HttpPost("{id}/change-state")]
        public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] ChangeOrderStatusDto dto)
        {
            var order = await _orderService.ChangeStatusAsync(id, dto);
            var response = new ApiResponseDto<OrderDto>(order, $"Order status changed to {order.Status}");
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] OrderFilterDto filter)
        {
            var orders = await _orderService.SearchOrdersAsync(filter);
            var response = new ApiResponseDto<OrderManagementAPI.Aplication.DTOs.PagedResult<OrderDto>>(orders, "Orders retrieved successfully");
            return Ok(response);
        }
    }
}
