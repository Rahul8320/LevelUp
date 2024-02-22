using System.Text.Json.Serialization;

namespace DotnetCoreWebApi.Models;

public class Product
{
    public int Id { get; set; }
    public string Sku { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; } = decimal.MinValue;
    public bool IsAvailable { get; set; } = false;

    public int CategoryId { get; set; }
    [JsonIgnore]
    public virtual Category Category { get; set; } = default!;
}
