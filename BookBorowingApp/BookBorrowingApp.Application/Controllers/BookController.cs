using System.Net;
using BookBoowingApp.Domain.Common;
using BookBoowingApp.Service.IServices;
using BookBoowingApp.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BookBorrowingApp.Application.Controllers;

/// <summary>
/// Books api controller.
/// </summary>
/// <param name="bookService">The interface for book service.</param>
[ApiController]
[Route("api/books")]
public class BookController(IBookService bookService) : ControllerBase
{
    /// <summary>
    /// The book service interface.
    /// </summary>
    private readonly IBookService _bookService = bookService;

    /// <summary>
    /// Get all available books for borrowing by other users.
    /// </summary>
    /// <returns>Return List of Books or 404.</returns>
    /// <exception cref="ApiException">Api Exception</exception>
    [HttpGet]
    public async Task<IActionResult> GetAllAvailableBooks()
    {
        try
        {
            // Fetching all available books details from service.
            var allBooks = await _bookService.GetAllAvailableBooks();

            // Check for empty book list.
            if (allBooks.Data.Count == 0)
            {
                return NotFound();
            }

            // Return the book list data with 200 Status code.
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
    /// Get single book details route.
    /// </summary>
    /// <param name="id">The id of the book.</param>
    /// <returns>Returns single book details or 404.</returns>
    /// <exception cref="ApiException">Api Exception</exception>
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> BookDetails(Guid id)
    {
        try
        {
            // Fetching book details from service.
            var bookDetails = await _bookService.GetBookDetails(id);

            // Check for not found response
            if (bookDetails.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }

            // Returns book details data with 200 status code.
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

    /// <summary>
    /// Create new book route.
    /// </summary>
    /// <param name="bookModel">The details of the new book.</param>
    /// <returns>Return the 201 response with newly created book data or 400. </returns>
    /// <exception cref="ApiException">Api Exception.</exception>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateNewBook([FromBody] AddBookModel bookModel)
    {
        try
        {
            // Check for model validation.
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // Creating new book.
            var bookDetails = await _bookService.AddNewBook(bookModel);

            // Check for 201 response code.
            if (bookDetails.StatusCode == HttpStatusCode.Created)
            {
                return StatusCode((int)bookDetails.StatusCode, bookDetails.Data);
            }

            // return the response code if anything happened wrong.
            return StatusCode((int)bookDetails.StatusCode);
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
    /// Update single book route.
    /// </summary>
    /// <param name="id">The id of the requested book.</param>
    /// <param name="bookModel">The updated book details.</param>
    /// <returns>Returns the updated book details or 404 or 400.</returns>
    /// <exception cref="ApiException">The Api Exception.</exception>
    [HttpPut]
    [Route("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateBookDetails(Guid id, [FromBody] AddBookModel bookModel)
    {
        try
        {
            // Check for model validations.
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // Updated the book details.
            var bookDetails = await _bookService.UpdateBookById(id, bookModel);

            // Check for 404 response code.
            if (bookDetails.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }

            // Check for 200 response code.
            if (bookDetails.StatusCode == HttpStatusCode.OK)
            {
                return Ok(bookDetails.Data);
            }

            // return the response code if anything goes wrong.
            return StatusCode((int)bookDetails.StatusCode);
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
    /// Delete book route.
    /// </summary>
    /// <param name="id">The book id.</param>
    /// <returns>Returns susses or not found response/</returns>
    /// <exception cref="ApiException">The Api Exception.</exception>
    [HttpDelete]
    [Route("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        try
        {
            // Updated the book details.
            var response = await _bookService.DeleteBookById(id);

            // Check for 404 response code.
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }

            // Check for 200 response code.
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok();
            }

            // return the response code if anything goes wrong.
            return StatusCode((int)response.StatusCode);
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

    [HttpGet]
    [Route("supported-genre")]
    [Authorize]
    public IActionResult GetSupportedGenres(Guid id)
    {
        try
        {
            // Updated the book details.
            var response = _bookService.GetSupportedGenre();

            // Check for 404 response code.
            if (response.Data.Count == 0 || response.Data.IsNullOrEmpty())
            {
                return NotFound();
            }

            return Ok(response.Data);
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
