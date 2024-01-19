using System.IdentityModel.Tokens.Jwt;
using System.Net;
using BookBoowingApp.Domain.Common;
using BookBoowingApp.Service.IServices;
using BookBoowingApp.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookBorrowingApp.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost]
    [Route("register")]
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

    [HttpPost]
    [Route("login")]
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

}
