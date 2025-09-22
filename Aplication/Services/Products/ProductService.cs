using Microsoft.EntityFrameworkCore;
using OrderManagementAPI.Aplication.DTOs;
using OrderManagementAPI.Aplication.DTOs.Products;
using OrderManagementAPI.Aplication.Exceptions;
using OrderManagementAPI.Aplication.Mappers;
using OrderManagementAPI.Domain.Entities;
using OrderManagementAPI.Infrastructure.Percistence;

namespace OrderManagementAPI.Aplication.Services.Products
{
    public class ProductService : IProductService
    {

        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var product = new Product(dto.Sku, dto.Name, dto.Price, dto.Type)
            {
                Description = dto.Description,
                Size = dto.Size,
                Color = dto.Color,
                CapacityMl = dto.CapacityMl,
                HeightCm = dto.HeightCm,
                WidthCm = dto.WidthCm,
                Paper = dto.Paper
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product.ToDto();
        }

        public async Task<ProductDto?> GetByIdAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            return product == null ? null : product.ToDto();
        }

        public async Task<PagedResult<ProductDto>> GetAllAsync(ProductFilterDto filter)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(p => p.Name.Contains(filter.Name));

            if (!string.IsNullOrEmpty(filter.SKU))
                query = query.Where(p => p.Sku.Contains(filter.SKU));

            if (!string.IsNullOrEmpty(filter.Description))
                query = query.Where(p => p.Description == filter.Description);

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(x => x.ToDto())
                .ToListAsync();

            return new PagedResult<ProductDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = filter.Page,
                PageSize = filter.PageSize
            };
        }

        public async Task<ProductDto> UpdateAsync(Guid id, CreateProductDto dto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                throw new BusinessException("Product not found");

            product.Sku = dto.Sku;
            product.Name = dto.Name;
            product.Description = dto.Description ?? "";
            product.Price = dto.Price;
            product.Type = dto.Type;
            product.Size = dto.Size;
            product.Color = dto.Color;
            product.CapacityMl = dto.CapacityMl;
            product.HeightCm = dto.HeightCm;
            product.WidthCm = dto.WidthCm;
            product.Paper = dto.Paper;

            await _context.SaveChangesAsync();

            return product.ToDto();
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                throw new BusinessException("Product not found");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }


    }
}
