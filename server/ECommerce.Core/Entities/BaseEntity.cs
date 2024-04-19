using System.ComponentModel.DataAnnotations;

namespace ECommerce.Core.Entities;

public class BaseEntity
{
    [Key]
    public long Id { get; set; }
}