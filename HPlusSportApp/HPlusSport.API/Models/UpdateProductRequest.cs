using System.ComponentModel.DataAnnotations;

namespace HPlusSport.API.Models;

public class UpdateProductRequest
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    [Range(1, double.MaxValue)]
    public decimal? Price { get; set; }

    public bool? IsAvailable { get; set; }
}
