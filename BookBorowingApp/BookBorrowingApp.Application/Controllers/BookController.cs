using System.Net;
using BookBoowingApp.Domain.Common;
using BookBoowingApp.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBorrowingApp.Application.Controllers;

[ApiController]
[Route("api/books")]
public class BookController(IBookService bookService) : ControllerBase
{
    private readonly IBookService _bookService = bookService;

    /// <summary>
    /// Get all available books for borrowing by other users.
    /// </summary>
    /// <returns>Return List of Books.</returns>
    /// <exception cref="ApiException">Api Exception</exception>
    [HttpGet]
    public async Task<IActionResult> GetAllAvailableBooks()
    {
        try
        {
            var allBooks = await _bookService.GetAllAvailableBooks();

            if (allBooks.Data.Count == 0)
            {
                return NotFound();
            }

            return Ok(allBooks.Data);
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
    /// Get single book details.
    /// </summary>
    /// <param name="id">The id of the book.</param>
    /// <returns>Returns single book details.</returns>
    /// <exception cref="ApiException">Api Exception</exception>
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> BookDetails(Guid id)
    {
        try
        {
            var bookDetails = await _bookService.GetBookDetails(id);

            if (bookDetails.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }

            return Ok(bookDetails.Data);
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
