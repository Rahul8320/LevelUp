namespace DotnetCoreWebApi.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public virtual List<Product> Products { get; set; } = [];
}
