using Microsoft.AspNetCore.Mvc;

namespace CloudCustomers.Api.Controllers;

[ApiController]
[Route("api/user")]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        return Ok("All Good");
    }
}
