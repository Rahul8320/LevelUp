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
        var users = await _userService.GetAllUsers();

        if (users != null && users.Count != 0)
        {
            return Ok(users);
        }

        return NotFound();

    }
}
