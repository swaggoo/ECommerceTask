using ECommerce.Core.Enums;

namespace ECommerce.Core.DTOs;

public class OrderTableModel
{
    public string OrderId { get; set; }
    public string CustomerFullname { get; set; }
    public string CustomerPhone { get; set; }
    public string Date { get; set; }
    public string TotalPrice { get; set; }
    public OrderStatus Status { get; set; }
}