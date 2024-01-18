using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBorrowingApp.Application.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    public IActionResult GetAllBooks()
    {
        var books = new List<string>() { "Deep Work", "$100M Dollar Company", "Rich Dad Poor Dad", "Ego is your enemy" };

        return Ok(books);
    }
}
