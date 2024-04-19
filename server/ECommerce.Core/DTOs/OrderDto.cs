using System.ComponentModel.DataAnnotations;

namespace ECommerce.Core.DTOs;

public record OrderDto(
    [Required]
    [MaxLength(100)]
    string CustomerFullName, 
    [Required]
    string CustomerPhone, 
    [Required]
    Dictionary<long, int> ProductQuantities);