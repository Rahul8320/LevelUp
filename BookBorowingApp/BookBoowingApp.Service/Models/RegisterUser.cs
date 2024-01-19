using System.ComponentModel.DataAnnotations;

namespace BookBoowingApp.Service.Models;

public class RegisterUser
{
    [Required(ErrorMessage = "Name can not be empty!")]
    [MinLength(3, ErrorMessage = "Name must be 3 character long!")]
    [MaxLength(15, ErrorMessage = "Name can not be more than 15 character long.")]
    public string Name { get; set; } = default!;

    [Required(ErrorMessage = "UserName can not be empty!")]
    [MinLength(3, ErrorMessage = "UserName must be 3 character long!")]
    [MaxLength(15, ErrorMessage = "UserName can not be more than 15 character long.")]
    public string UserName { get; set; } = default!;

    [Required(ErrorMessage = "Email can not be empty!")]
    [EmailAddress(ErrorMessage = "Please input an valid email address!")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Password can not be empty!")]
    [MinLength(8, ErrorMessage = "Password must be 8 character long!")]
    [MaxLength(20, ErrorMessage = "Password can not be more than 20 character long.")]
    public string Password { get; set; } = default!;
}
