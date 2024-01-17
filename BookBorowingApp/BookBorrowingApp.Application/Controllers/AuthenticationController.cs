using BookBorrowingApp.Application.Models;
using BookBorrowingApp.Application.Models.Authentication.Signup;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookBorrowingApp.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController(
    UserManager<IdentityUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IConfiguration configuration) : ControllerBase
{
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly IConfiguration _configuration = configuration;

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
    {
        // Check for valid register user model
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Check for existing user
        var existingUser = await _userManager.FindByEmailAsync(registerUser.Email);

        if (existingUser != null)
        {
            return StatusCode(StatusCodes.Status403Forbidden, new Response
            {
                Status = "Error",
                Message = "User Already Exists!"
            });
        }

        // Add the user in the database
        IdentityUser user = new()
        {
            Email = registerUser.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = registerUser.UserName
        };
        var result = await _userManager.CreateAsync(user, registerUser.Password);

        if (result.Succeeded)
        {
            return StatusCode(StatusCodes.Status201Created, new Response
            {
                Status = "Success",
                Message = "User Register Successfully."
            });
        }
        else
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response
            {
                Status = "Error",
                Message = "Register Failed!"
            });
        }
    }
}
