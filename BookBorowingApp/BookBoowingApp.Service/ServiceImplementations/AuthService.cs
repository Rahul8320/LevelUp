using System.IdentityModel.Tokens.Jwt;
using System.Net;
using BookBoowingApp.Domain.Common;
using BookBoowingApp.Infrastructure;
using BookBoowingApp.Service.IServices;
using BookBoowingApp.Service.Models;
using BookBorrowingApp.Domain.Entities;

namespace BookBoowingApp.Service.ServiceImplementations;

public class AuthService(IAuthRepository authRepository) : IAuthService
{
    private readonly IAuthRepository _authRepository = authRepository;

    public async Task<ServiceResult> Login(LoginUser loginUser)
    {
        // Check for user is exists or not
        var existingUser = await _authRepository.GetUserByUserName(loginUser.UserName);

        if (existingUser == null)
        {
            return new ServiceResult(HttpStatusCode.Unauthorized);
        }

        // check the password is valid
        var result = await _authRepository.IsValidPassword(existingUser, loginUser.Password);

        if (!result)
        {
            return new ServiceResult(HttpStatusCode.Unauthorized);
        }

        var jwtToken = await _authRepository.CreateLoginToken(existingUser);

        return new ServiceResult<JwtSecurityToken>(HttpStatusCode.OK, jwtToken);
    }

    public async Task<ServiceResult> Register(RegisterUser registerUser)
    {
        try
        {
            // Check for existing user
            var existingUser = await _authRepository.GetUserByEmail(registerUser.Email);

            if (existingUser != null)
            {
                return new ServiceResult(HttpStatusCode.Conflict, "User Already Exists!");
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
            throw new ApiException(HttpStatusCode.InternalServerError, ex.Message);
        }

    }
}
