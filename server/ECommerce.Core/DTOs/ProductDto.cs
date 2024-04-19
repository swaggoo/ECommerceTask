using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Core.DTOs;

public record ProductDto(
    [Required] [MaxLength(100)] 
    string Name, 
    [Required]
    decimal Price, 
    [Required] string Code,
    [Required] IFormFile Image);