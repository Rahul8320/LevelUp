using System.ComponentModel.DataAnnotations;

namespace BookBorrowingApp.Application.Models.Authentication.Signup;

public class RegisterUser
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = default!;

    [Required(ErrorMessage = "UserName is required")]
    public string UserName { get; set; } = default!;

    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = default!;
}
