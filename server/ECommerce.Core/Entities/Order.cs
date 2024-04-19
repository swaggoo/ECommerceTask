using System.Text.Json.Serialization;
using ECommerce.Core.Enums;

namespace ECommerce.Core.Entities;

public class Order : BaseEntity
{
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public string CustomerFullName { get; set; }
    public string CustomerPhone { get; set; }
    public OrderStatus OrderStatus { get; set; }
    
    [JsonIgnore] public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}