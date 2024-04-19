
using ECommerce.API.Extensions;
using ECommerce.Core.DTOs;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using ECommerce.Services.Helpers.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(IOrderService orderService) : ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await orderService.GetAllOrdersAsync();

        return Ok(orders);
    }
    
    [HttpGet("perPage")]
    public async Task<ActionResult<PagedList<OrderTableModel>>> GetOrdersPerPage([FromQuery] UserParams userParams)
    {
        var orders = await orderService.GetOrdersPerPageAsync(userParams);

        Response.AddPaginationHeader(new PaginationHeader(orders.CurrentPage, orders.TotalPages, orders.PageSize, orders.TotalCount));
        
        return Ok(orders);
    }
    
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetOrder(long id)
    {
        var order = await orderService.GetOrderByIdAsync(id);

        if (order is null)
        {
            return NotFound();
        }

        return Ok(order);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateOrder(OrderDto orderDto, CancellationToken cancellationToken)
    {
        await orderService.CreateOrderAsync(orderDto, cancellationToken);

        return NoContent();
    }
}