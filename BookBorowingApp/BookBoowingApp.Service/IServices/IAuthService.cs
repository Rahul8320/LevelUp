using System.IdentityModel.Tokens.Jwt;
using BookBoowingApp.Domain.Common;
using BookBoowingApp.Service.DTOs;
using BookBoowingApp.Service.Models;

namespace BookBoowingApp.Service.IServices;

/// <summary>
/// Represents the interface for auth service.
/// </summary>
public interface IAuthService
{

    /// <summary>
    /// Create new user
    /// </summary>
    /// <param name="registerUser">New user details.</param>
    /// <returns>Returns service result indicating the result of this operation.</returns>
    Task<ServiceResult> Register(RegisterUser registerUser);

    /// <summary>
    /// User login.
    /// </summary>
    /// <param name="loginUser">User login credentials.</param>
    /// <returns>Returns jwt token.</returns>
    Task<ServiceResult<JwtSecurityToken>> Login(LoginUser loginUser);

    /// <summary>
    /// Get logged in user data.
    /// </summary>
    /// <returns>Return authenticated user dto.</returns>
    AuthenticatedUserDTO GetAuthenticatedUserData();
}
