using System.ComponentModel.DataAnnotations;

namespace HPlusSport.API.Models;

public class CreateProductRequest
{
    [Required]
    public string Sku { get; set; } = string.Empty;
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
    [Range(1, double.MaxValue)]
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; } = true;

    [Required]
    public int CategoryId { get; set; }
}
