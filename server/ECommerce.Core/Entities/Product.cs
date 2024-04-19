using System.Text.Json.Serialization;

namespace ECommerce.Core.Entities;

public class Product : BaseEntity
{
    public string Code { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImagePath { get; set; }
    public string ImagePublicId { get; set; }
    [JsonIgnore] public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}