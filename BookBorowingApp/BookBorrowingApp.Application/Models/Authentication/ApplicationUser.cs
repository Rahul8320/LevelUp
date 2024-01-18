using Microsoft.AspNetCore.Identity;

namespace BookBorrowingApp.Application.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = default!;
    public int Tokens_Available { get; set; } = default!;
    public List<Guid> Books_Borrowed { get; set; } = default!;
    public List<Guid> Books_Lent { get; set; } = default!;
}
