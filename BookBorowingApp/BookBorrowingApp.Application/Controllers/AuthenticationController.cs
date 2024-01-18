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

            return StatusCode((int)response.StatusCode, response);

        }
        catch (ApiException ex)
        {
            return StatusCode((int)ex.StatusCode, ex.Data);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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

            return Ok(response);
        }
        catch (ApiException ex)
        {
            return StatusCode((int)ex.StatusCode, ex.Data);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}
