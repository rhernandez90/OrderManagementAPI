using OrderManagementAPI.Aplication.DTOs;
using OrderManagementAPI.Aplication.DTOs.Products;

namespace OrderManagementAPI.Aplication.Services.Products
{
    public interface IProductService
    {
        Task<ProductDto> CreateAsync(CreateProductDto dto);
        Task<ProductDto?> GetByIdAsync(Guid id);
        Task<PagedResult<ProductDto>> GetAllAsync(ProductFilterDto filter);
        Task<ProductDto> UpdateAsync(Guid id, CreateProductDto dto);
        Task DeleteAsync(Guid id);
    }
}
