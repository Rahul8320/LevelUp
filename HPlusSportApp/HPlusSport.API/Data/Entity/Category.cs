using System.ComponentModel.DataAnnotations;

namespace HPlusSport.API.Data.Entity;

public class Category
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public virtual List<Product> Products { get; set; } = [];
}
