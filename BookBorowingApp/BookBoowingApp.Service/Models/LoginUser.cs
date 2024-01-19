using System.ComponentModel.DataAnnotations;

namespace BookBoowingApp.Service.Models;

public class LoginUser
{
    [Required(ErrorMessage = "Username can not be empty!")]
    public string UserName { get; set; } = default!;

    [Required(ErrorMessage = "Password can not be empty!")]
    public string Password { get; set; } = default!;
}
