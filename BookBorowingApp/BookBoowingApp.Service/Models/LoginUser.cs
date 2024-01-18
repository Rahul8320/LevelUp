using System.ComponentModel.DataAnnotations;

namespace BookBoowingApp.Service.Models;

public class LoginUser
{
    [Required(ErrorMessage = "Username is required.")]
    public string UserName { get; set; } = default!;

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; } = default!;
}
