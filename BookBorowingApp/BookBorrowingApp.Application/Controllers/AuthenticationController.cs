using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookBorrowingApp.Application.Models;
using BookBorrowingApp.Application.Models.Authentication.Login;
using BookBorrowingApp.Application.Models.Authentication.Signup;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BookBorrowingApp.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController(
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IConfiguration configuration) : ControllerBase
{
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IConfiguration _configuration = configuration;

    [HttpPost]
    [Route("register")]
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
        var result = await _userManager.CreateAsync(user, registerUser.Password);

        if (!result.Succeeded)
        {
            Console.WriteLine(result);
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse<List<IdentityError>>
            {
                Status = "Failed",
                Message = "User Register Failed!",
                Errors = result.Errors.ToList()
            });
        }

        if (await _roleManager.RoleExistsAsync("User"))
        {
            await _userManager.AddToRoleAsync(user, "User");
        }

        return StatusCode(StatusCodes.Status201Created, new Response
        {
            Status = "Success",
            Message = "User Register Successfully."
        });
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginUser loginUser)
    {
        // Check for validation errors
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Check for user is exists or not
        var existingUser = await _userManager.FindByNameAsync(loginUser.UserName);

        if (existingUser == null)
        {
            return Unauthorized();
        }

        // Check for password is match or not
        if (await _userManager.CheckPasswordAsync(existingUser, loginUser.Password))
        {
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, existingUser.UserName!),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userRoles = await _userManager.GetRolesAsync(existingUser);

            foreach (var role in userRoles)
            {
                authClaims.Add(new(ClaimTypes.Role, role));
            }

            var jwtToken = GetToken(authClaims);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                expiration = jwtToken.ValidTo
            });
        }

        return Unauthorized();
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
