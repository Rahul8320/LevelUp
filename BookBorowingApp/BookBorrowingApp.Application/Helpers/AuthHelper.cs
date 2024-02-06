using System.Security.Claims;
using BookBoowingApp.Service.DTOs;

namespace BookBorrowingApp.Application.Helpers;

public class AuthHelper(IHttpContextAccessor httpContextAccessor)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    /// <summary>
    /// Get current authenticated user data
    /// </summary>
    /// <returns>Return AuthenticatedUserDTO</returns>
    internal AuthenticatedUserDTO GetUserData()
    {
        AuthenticatedUserDTO authenticatedUserDTO = new();

        var claimsIdentity = _httpContextAccessor.HttpContext?.User?.Identities.FirstOrDefault(item => item.Claims.Any());
        var claims = claimsIdentity?.Claims;

        authenticatedUserDTO.Email = claims?.FirstOrDefault(Item => Item.Type == ClaimTypes.Email)?.Value;
        authenticatedUserDTO.UserId = claims?.FirstOrDefault(item => item.Type == ClaimTypes.GivenName)?.Value;
        authenticatedUserDTO.UserName = claims?.FirstOrDefault(item => item.Type == ClaimTypes.Name)?.Value;
        authenticatedUserDTO.Role = claims?.FirstOrDefault(item => item.Type == ClaimTypes.Role)?.Value;

        return authenticatedUserDTO;
    }
}
