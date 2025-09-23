using Microsoft.AspNetCore.Mvc;
using OrderManagementAPI.Aplication.DTOs;
using OrderManagementAPI.Aplication.DTOs.Products;
using OrderManagementAPI.Aplication.Services.Products;
using OrderManagementAPI.Controllers.DTOs;
using OrderManagementAPI.Domain.Entities;

namespace OrderManagementAPI.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            var product = await _productService.CreateAsync(dto);
            return Ok(new ApiResponseDto<ProductDto>(product, "Product created successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            return Ok(new ApiResponseDto<ProductDto?>(product, "Product retrieved successfully"));
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<Product>>> GetAll([FromQuery] ProductFilterDto filter)
        {
            var result = await _productService.GetAllAsync(filter);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateProductDto dto)
        {
            var product = await _productService.UpdateAsync(id, dto);
            return Ok(new ApiResponseDto<ProductDto>(product, "Product updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _productService.DeleteAsync(id);
            return Ok(new ApiResponseDto<object>(data: null, "Product deleted successfully"));
        }
    }
}
