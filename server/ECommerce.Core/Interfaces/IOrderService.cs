using ECommerce.Core.DTOs;
using ECommerce.Core.Entities;
using ECommerce.Services.Helpers.Pagination;

namespace ECommerce.Core.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<PagedList<OrderTableModel>> GetOrdersPerPageAsync(UserParams userParams);
    Task<Order?> GetOrderByIdAsync(long id);
    Task CreateOrderAsync(OrderDto orderDto, CancellationToken cancellationToken);
}