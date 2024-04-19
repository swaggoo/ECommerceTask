using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ECommerce.Core.Entities;

public class OrderProduct : BaseEntity
{
    [ForeignKey(nameof(Entities.Order))] public long OrderId { get; set; }
    [JsonIgnore] public virtual Order Order { get; set; }

    [ForeignKey(nameof(Entities.Product))] public long ProductId { get; set; }
    [JsonIgnore] public virtual Product Product { get; set; }

    public int Amount { get; set; }

    public decimal TotalPrice { get; set; }
}