using ECommerce.API.Extensions;
using ECommerce.Core.DTOs;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using ECommerce.Services.Helpers.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetProducts()
    {
        var products = await productService.GetAllProductsAsync();

        return Ok(products);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetProductsPerPage([FromQuery] UserParams userParams)
    {
        var products = await productService.GetProductsPerPageAsync(userParams);

        Response.AddPaginationHeader(new PaginationHeader(products.CurrentPage, products.TotalPages, products.PageSize, products.TotalCount));
        
        return Ok(products);
    }
    
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetProduct(long id)
    {
        var product = await productService.GetProductAsync(id);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }
    
    [HttpGet("{code}")]
    public async Task<IActionResult> GetProductByCode(string code)
    {
        var product = await productService.GetProductByCodeAsync(code);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromForm]ProductDto productDto)
    {
        var success = await productService.AddProductAsync(productDto);

        if (!success)
        {
            return BadRequest();
        }
        
        return NoContent();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateProduct(Product product)
    {
        await productService.UpdateProductAsync(product);

        return NoContent();
    }
}