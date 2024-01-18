using System.ComponentModel.DataAnnotations;

namespace BookBorrowingApp.Application.Models.Authentication.Login;

public class LoginUser
{
    [Required(ErrorMessage = "Username is required.")]
    public string UserName { get; set; } = default!;

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; } = default!;
}
