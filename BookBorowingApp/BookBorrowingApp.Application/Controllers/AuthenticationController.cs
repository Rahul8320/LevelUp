using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mime;
using BookBoowingApp.Domain.Common;
using BookBoowingApp.Service.IServices;
using BookBoowingApp.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBorrowingApp.Application.Controllers;

/// <summary>
/// Auth api controller.
/// </summary>
/// <param name="authService">The auth service interface</param>
[ApiController]
[Route("api/[controller]")]
public class AuthenticationController(IAuthService authService) : ControllerBase
{
    /// <summary>
    /// Represents the auth service interface.
    /// </summary>
    private readonly IAuthService _authService = authService;

    /// <summary>
    /// Register new user
    /// </summary>
    /// <param name="registerUser">The register user data.</param>
    /// <returns>Returns result of the register action.</returns>
    /// <exception cref="ApiException">The api exception</exception>
    [HttpPost]
    [Route("register")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
    {
        try
        {
            // Check for valid register user model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authService.Register(registerUser);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(response.ValidationError);
            }

            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                return Conflict(response.ValidationError);
            }

            return StatusCode((int)response.StatusCode, new { response.StatusCode, response.Message });

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
    /// Login user.
    /// </summary>
    /// <param name="loginUser">The login user model.</param>
    /// <returns>Returns the result of this login action.</returns>
    /// <exception cref="ApiException">The api exception.</exception>
    [HttpPost]
    [Route("login")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> Login([FromBody] LoginUser loginUser)
    {
        try
        {
            // Check for validation errors
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // get jwt token
            var response = await _authService.Login(loginUser);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return Unauthorized();
            }

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(response.Data),
                expiration = response.Data.ValidTo
            });
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
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ApiException"></exception>
    [HttpGet]
    [Route("verify")]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    public IActionResult Verify()
    {
        try
        {
            var data = _authService.GetAuthenticatedUserData();

            return Ok(data);
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
