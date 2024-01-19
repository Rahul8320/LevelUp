namespace BookBoowingApp.Service.DTOs;

public class AuthenticatedUserDTO
{
    public string? UserName { get; set; } = default!;
    public string? Email { get; set; } = default!;
    public string? Id { get; set; } = default!;
}
