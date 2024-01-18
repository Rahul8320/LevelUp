using System.IdentityModel.Tokens.Jwt;
using BookBorrowingApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookBoowingApp.Infrastructure;

public interface IAuthRepository
{
    public Task<ApplicationUser?> GetUserByEmail(string email);

    public Task<ApplicationUser?> GetUserByUserName(string username);
    public Task<IdentityResult> CreateUser(ApplicationUser applicationUser, string password);

    public Task<JwtSecurityToken> CreateLoginToken(ApplicationUser applicationUser);

    public Task<bool> IsValidPassword(ApplicationUser applicationUser, string password);

}
