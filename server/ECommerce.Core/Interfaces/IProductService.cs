using ECommerce.Core.DTOs;
using ECommerce.Core.Entities;
using ECommerce.Services.Helpers.Pagination;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Core.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductAsync(long id);
    Task<Product?> GetProductByCodeAsync(string code);
    Task<PagedList<Product>> GetProductsPerPageAsync(UserParams userParams);
    Task<bool> AddProductAsync(ProductDto product);
    Task UpdateProductAsync(Product product);
}