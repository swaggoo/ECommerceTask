using AutoMapper;
using ECommerce.Core.DTOs;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using ECommerce.Services.Helpers.Pagination;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Services;

public class OrderService(IMapper mapper, IRepository repository) : IOrderService
{
    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await repository.GetAll<Order>().ToListAsync();
    }
 
    public Task<PagedList<OrderTableModel>> GetOrdersPerPageAsync(UserParams userParams)
    {
        var orderProducts = repository.GetAll<OrderProduct>()
            .Include(op => op.Order)
            .ThenInclude(o => o.Products);
    
        var orderModels = mapper.ProjectTo<OrderTableModel>(orderProducts);

        return PagedList<OrderTableModel>.CreateAsync(orderModels, userParams.PageNumber, userParams.PageSize);
    }
    
    public async Task<Order?> GetOrderByIdAsync(long id)
    {
        return await repository.GetAsync<Order>(o => o.Id == id);
    }
    
    public async Task CreateOrderAsync(OrderDto orderDto, CancellationToken cancellationToken)
    {
        var productQuantities = orderDto.ProductQuantities;

        var productIds = productQuantities.Keys.ToList();
        var products = await repository
            .GetAll<Product>()
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync(cancellationToken);

        var order = mapper.Map<Order>(orderDto);

        foreach (var product in products)
        {
            var quantity = productQuantities[product.Id];

            var orderProduct = new OrderProduct
            {
                Product = product,
                Order = order,
                Amount = quantity,
                TotalPrice = product.Price * quantity
            };

            repository.Insert(orderProduct);
        }

        repository.Insert(order);

        await repository.SaveChangesAsync(cancellationToken);
    }
}