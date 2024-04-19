using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ECommerce.Core.DTOs;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using ECommerce.Services.Helpers;
using ECommerce.Services.Helpers.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ECommerce.Services.Services;

public class ProductService : IProductService
{
    private readonly IRepository _repository;
    private readonly IMapperBase _mapper;
    private readonly Cloudinary _cloudinary;

    public ProductService(IRepository repository, IMapper mapper
        , IOptions<CloudinarySettings> config)
    {
        _repository = repository;
        _mapper = mapper;
        var acc = new Account(
            config.Value.CloudName,
            config.Value.ApiKey,
            config.Value.ApiSecret);
        _cloudinary = new Cloudinary(acc);
    }
    
    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _repository.GetAll<Product>().ToListAsync();
    }
    
    public async Task<PagedList<Product>> GetProductsPerPageAsync(UserParams userParams)
    {
        var products = _repository.GetAll<Product>().AsQueryable();
        return await PagedList<Product>.CreateAsync(products, userParams.PageNumber, userParams.PageSize);
    }
    
    public async Task<Product?> GetProductAsync(long id)
    {
        return await _repository.GetAsync<Product>(p => p.Id == id);
    }
    
    public async Task<Product?> GetProductByCodeAsync(string code)
    {
        return await _repository.GetAsync<Product>(p => p.Code == code);
    }
    
    public async Task<bool> AddProductAsync(ProductDto productDto)
    {
        var result = await UploadImage(productDto.Image);
        if (result.Error != null)
        {
            return false;
        }
        
        var product = _mapper.Map<Product>(productDto);
        product.ImagePath = result.SecureUrl.AbsoluteUri;
        product.ImagePublicId = result.PublicId;
        
        _repository.Insert(product);
        await _repository.SaveChangesAsync();

        return true;
    }
    
    public async Task UpdateProductAsync(Product product)
    {
        _repository.Update(product);
        await _repository.SaveChangesAsync();
    }
    
    private async Task<ImageUploadResult> UploadImage(IFormFile formFile)
    {
        await using var stream = formFile.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(formFile.FileName, stream),
            Transformation = new Transformation().Height(500).Width(500).Crop("fill"),
            Folder = "ECommerceTask"
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        return uploadResult;
    }
}