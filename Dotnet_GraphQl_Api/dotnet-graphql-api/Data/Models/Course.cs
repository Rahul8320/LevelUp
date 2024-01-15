using System.ComponentModel.DataAnnotations;

namespace dotnet_graphql_api.Data.Models;

public class Course
{
    [Key]
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Review { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
