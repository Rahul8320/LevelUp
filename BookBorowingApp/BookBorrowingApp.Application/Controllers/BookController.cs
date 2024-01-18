using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBorrowingApp.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{

    public BookController()
    {
        
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetAllBooks()
    {
        var books = new List<string>() { "Deep Work", "$100M Dollar Company", "Rich Dad Poor Dad", "Ego is your enemy" };

        return Ok(books);
    }

    [HttpGet]
    [Route("test")]
    public IActionResult Test()
    {
        return Ok("Test");
    }
}
