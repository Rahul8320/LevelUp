using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetProducts()
    {
        return Ok();
    }
}
