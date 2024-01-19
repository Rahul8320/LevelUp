using System.ComponentModel.DataAnnotations;

namespace BookBoowingApp.Service.Models;

public class AddBookModel
{
    [Required(ErrorMessage = "Name can not be empty!")]
    [MinLength(3, ErrorMessage = "Name must be 3 character long!")]
    [MaxLength(20, ErrorMessage = "Name can not be more than 20 character long!")]
    public string Name { get; set; } = default!;

    [Required(ErrorMessage = "Cover Image can not be empty!")]
    [MinLength(10)]
    public string CoverImage { get; set; } = default!;
    public List<string>? Images { get; set; } = default!;

    [Required(ErrorMessage = "Author can not be empty!")]
    [MinLength(3, ErrorMessage = "Author must be 3 character long!")]
    [MaxLength(15, ErrorMessage = "Author can not be more than 15 character long!")]
    public string Author { get; set; } = default!;

    [Required(ErrorMessage = "Genre can not be empty!")]
    public string Genre { get; set; } = default!;

    [Required(ErrorMessage = "Description can not be empty!")]
    [MinLength(10, ErrorMessage = "Description must be 10 character long!")]
    [MaxLength(500, ErrorMessage = "Description can not be more than 500 character long!")]
    public string Description { get; set; } = default!;
}
