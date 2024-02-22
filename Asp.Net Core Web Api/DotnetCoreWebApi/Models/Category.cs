using System.ComponentModel.DataAnnotations;

namespace DotnetCoreWebApi.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public virtual List<Product> Products { get; set; } = [];
}
