using System.ComponentModel.DataAnnotations;

namespace dotnet_graphql_api.Data.Models;

public class AddCourseDto
{
    [Required(ErrorMessage = "Name can not be empty.")]
    [MinLength(5, ErrorMessage = "Name must be 5 character long.")]
    public string Name { get; set; } = default!;

    [Required(ErrorMessage = "Description can not be empty.")]
    [MinLength(10, ErrorMessage = "Description must be 10 character long.")]
    public string Description { get; set; } = default!;

    [Required(ErrorMessage = "Review can not be empty.")]
    [Range(1,5, ErrorMessage = "Review must be between 1 to 5.")]
    public int Review { get; set; } = default!;
}
