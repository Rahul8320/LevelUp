using System.ComponentModel.DataAnnotations;

namespace dotnet_graphql_api.Data.Models;

public class AddCourseDto
{
    [Required(ErrorMessage = "Please input course Name.")]
    public string Name { get; set; } = default!;

    [Required(ErrorMessage = "Please input course Description.")]
    public string Description { get; set; } = default!;

    [Required(ErrorMessage = "Please input course Review.")]
    public int Review { get; set; }
}
