using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using BookBoowingApp.Domain.Common;
using BookBoowingApp.Infrastructure;
using BookBoowingApp.Service.DTOs;
using BookBoowingApp.Service.IServices;
using BookBoowingApp.Service.Models;
using BookBorrowingApp.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace BookBoowingApp.Service.ServiceImplementations;

/// <summary>
/// Represents the implementation of auth service interface.
/// </summary>
/// <param name="authRepository">The interface of auth repository.</param>
public class AuthService(IAuthRepository authRepository, IHttpContextAccessor httpContextAccessor) : IAuthService
{
    /// <summary>
    /// Represents the auth repository interface.
    /// </summary>
    private readonly IAuthRepository _authRepository = authRepository;

    /// <summary>
    /// Represents http context accessor.
    /// </summary>
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    /// <summary>
    /// User login.
    /// </summary>
    /// <param name="loginUser">User login credentials.</param>
    /// <returns>Returns jwt token.</returns>
    /// <exception cref="ApiException">The api exception.</exception>
    public async Task<ServiceResult<JwtSecurityToken>> Login(LoginUser loginUser)
    {
        try
        {
            // Fetch user data.
            var existingUser = await _authRepository.GetUserByUserName(loginUser.UserName);

            // Check for user is exists or not
            if (existingUser == null)
            {
                return new ServiceResult<JwtSecurityToken>(HttpStatusCode.Unauthorized);
            }

            // check the password is valid
            var result = await _authRepository.IsValidPassword(existingUser, loginUser.Password);

            if (!result)
            {
                return new ServiceResult<JwtSecurityToken>(HttpStatusCode.Unauthorized);
            }

            // get jwt token
            var jwtToken = await _authRepository.CreateLoginToken(existingUser);

            return new ServiceResult<JwtSecurityToken>(HttpStatusCode.OK, jwtToken);
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

    /// <summary>
    /// Create new user
    /// </summary>
    /// <param name="registerUser">New user details.</param>
    /// <returns>Returns service result indicating the result of this operation.</returns>
    /// <exception cref="ApiException">The api exception.</exception>
    public async Task<ServiceResult> Register(RegisterUser registerUser)
    {
        try
        {
            // Fetch user data.
            var existingUser = await _authRepository.GetUserByEmail(registerUser.Email);

            // Check for existing user
            if (existingUser != null)
            {
                return new ServiceResult<ValidationError>(HttpStatusCode.Conflict, new ValidationError(code: "Register Failed!", description: "User Already Exists!"));
            }

            // Create new application User
            ApplicationUser user = new()
            {
                Email = registerUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerUser.UserName,
                Name = registerUser.Name,
                Tokens_Available = 10,
                Books_Borrowed = [],
                Books_Lent = []
            };

            // Create New User in database
            var result = await _authRepository.CreateUser(user, registerUser.Password);

            // check for success result.
            if (!result.Succeeded)
            {
                var error = result.Errors.FirstOrDefault();

                if (error == null)
                {
                    return new ServiceResult(HttpStatusCode.InternalServerError, "User Register Failed!");
                }

                return new ServiceResult<ValidationError>(HttpStatusCode.BadRequest, new ValidationError(error.Code, error.Description));
            }

            return new ServiceResult(HttpStatusCode.Created, "User Register Successfully.");
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

    /// <summary>
    /// Get logged in user data.
    /// </summary>
    /// <returns>Return authenticated user dto.</returns>
    /// <exception cref="ApiException">The api exception</exception>
    public AuthenticatedUserDTO GetAuthenticatedUserData()
    {
        try
        {
            // initialize auth user dto
            AuthenticatedUserDTO authenticatedUserDTO = new();

            // Fetch claims identity from http context
            var claimsIdentity = _httpContextAccessor.HttpContext?.User?.Identities.FirstOrDefault(item => item.Claims.Any());
            var claims = claimsIdentity?.Claims;

            // Get claims value
            authenticatedUserDTO.Email = claims?.FirstOrDefault(Item => Item.Type == ClaimTypes.Email)?.Value;
            authenticatedUserDTO.UserId = claims?.FirstOrDefault(item => item.Type == ClaimTypes.GivenName)?.Value;
            authenticatedUserDTO.UserName = claims?.FirstOrDefault(item => item.Type == ClaimTypes.Name)?.Value;
            authenticatedUserDTO.Role = claims?.FirstOrDefault(item => item.Type == ClaimTypes.Role)?.Value;

            // check for user details is exists or not
            if (authenticatedUserDTO.UserId == null ||
                authenticatedUserDTO.Role == null ||
                authenticatedUserDTO.UserName == null ||
                authenticatedUserDTO.Email == null)
            {
                throw new ApiException(HttpStatusCode.InternalServerError, new Exception("Does not get user credentials!"));
            }

            // return the auth user data.
            return authenticatedUserDTO;
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
}
