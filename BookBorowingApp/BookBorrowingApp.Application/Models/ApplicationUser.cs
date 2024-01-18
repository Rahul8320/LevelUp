using Microsoft.AspNetCore.Identity;

namespace BookBorrowingApp.Application.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = default!;
    public int Tokens_Available { get; set; } = 0;
    public int Books_Borrowed { get; set; } = 0;
    public int Books_Lent { get; set; } = 0;
}
