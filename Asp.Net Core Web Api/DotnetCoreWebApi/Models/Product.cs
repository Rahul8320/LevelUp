using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DotnetCoreWebApi.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    public string Sku { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; } = decimal.MinValue;
    public bool IsAvailable { get; set; } = true;

    [Required]
    public int CategoryId { get; set; }
    [JsonIgnore]
    public virtual Category? Category { get; set; }
}
