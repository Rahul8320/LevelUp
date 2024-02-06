
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using BookBoowingApp.Domain.Common;
using BookBoowingApp.Domain.Enums;
using BookBorrowingApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookBoowingApp.Infrastructure.RepositoryImplementations;

public class AuthRepository(UserManager<ApplicationUser> userManager,
RoleManager<IdentityRole> roleManager,
IConfiguration configuration) : IAuthRepository
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly IConfiguration _configuration = configuration;

    public async Task<JwtSecurityToken> CreateLoginToken(ApplicationUser applicationUser)
    {
        try
        {
            // create claim list
            var authClaims = new List<Claim>
                {
                    new(ClaimTypes.Name, applicationUser.UserName!),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new(ClaimTypes.Email, applicationUser.Email!),
                    new(ClaimTypes.GivenName, applicationUser.Id)
                };

            // get user role
            var userRoles = await _userManager.GetRolesAsync(applicationUser);

            foreach (var role in userRoles)
            {
                authClaims.Add(new(ClaimTypes.Role, role));
            }

            // generate jwt token
            var jwtToken = GetToken(authClaims);

            return jwtToken;
        }
        catch (ApiException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ApiException(HttpStatusCode.InternalServerError, ex);
        }

    }

    public async Task<IdentityResult> CreateUser(ApplicationUser applicationUser, string password)
    {
        try
        {
            // Create new user
            var result = await _userManager.CreateAsync(applicationUser, password);

            if (!result.Succeeded)
            {
                return result;
            }

            // Assign User Role to user
            if (await _roleManager.RoleExistsAsync(UserRole.User.ToString()))
            {
                await _userManager.AddToRoleAsync(applicationUser, UserRole.User.ToString());
            }

            return result;
        }
        catch (ApiException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ApiException(HttpStatusCode.InternalServerError, ex);
        }
    }

    public async Task<ApplicationUser?> GetUserByEmail(string email)
    {
        try
        {
            return await _userManager.FindByEmailAsync(email);
        }
        catch (ApiException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ApiException(HttpStatusCode.InternalServerError, ex);
        }
    }

    public async Task<ApplicationUser?> GetUserByUserName(string username)
    {
        try
        {
            return await _userManager.FindByNameAsync(username);
        }
        catch (ApiException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ApiException(HttpStatusCode.InternalServerError, ex);
        }
    }

    public async Task<bool> IsValidPassword(ApplicationUser applicationUser, string password)
    {
        try
        {
            // Check for password is match or not
            var result = await _userManager.CheckPasswordAsync(applicationUser, password);

            return result;
        }
        catch (ApiException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ApiException(HttpStatusCode.InternalServerError, ex);
        }
    }

    // Get Jwt Token
    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(1),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }
}
