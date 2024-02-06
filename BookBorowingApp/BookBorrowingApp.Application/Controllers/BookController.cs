using System.Net;
using System.Net.Mime;
using BookBoowingApp.Domain.Common;
using BookBoowingApp.Service.IServices;
using BookBoowingApp.Service.Models;
using BookBorrowingApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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
    /// <response code="200">Returns the list of books.</response>
    /// <response code="404">Not found error if no books found.</response>
    /// <response code="500">Internal server error.</response>
    /// <exception cref="ApiException">Api Exception</exception>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<Book>))]
    [ProducesResponseType(404, Type = typeof(NotFound))]
    [ProducesResponseType(500, Type = typeof(ErrorResponse))]
    [Produces(MediaTypeNames.Application.Json)]
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
    /// <response code="200">Returns the book details.</response>
    /// <response code="404">Not found error if no book found of specific id.</response>
    /// <response code="500">Internal server error.</response>
    /// <exception cref="ApiException">Api Exception</exception>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(200, Type = typeof(Book))]
    [ProducesResponseType(404, Type = typeof(NotFound))]
    [ProducesResponseType(500, Type = typeof(ErrorResponse))]
    [Produces(MediaTypeNames.Application.Json)]
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
    /// <response code="200">Returns the newly created book.</response>
    /// <response code="400">Bad request with validation errors.</response>
    /// <response code="500">Internal server error.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <exception cref="ApiException">Api Exception.</exception>
    [HttpPost]
    [Authorize]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Book))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequest))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> CreateNewBook([FromBody] AddBookModel bookModel)
    {
        try
        {
            // Check for model validation.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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
    /// <response code="200">Returns the updated book details.</response>
    /// <response code="400">Bad request with validation errors.</response>
    /// <response code="404">Not found error if no book found of specific id.</response>
    /// <response code="500">Internal server error.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <exception cref="ApiException">The Api Exception.</exception>
    [HttpPut]
    [Route("{id}")]
    [Authorize]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Book))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequest))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFound))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [Produces(MediaTypeNames.Application.Json)]
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
    /// <response code="200">Returns success response.</response>
    /// <response code="404">Not found error if no book found with the specific id.</response>
    /// <response code="500">Internal server error.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <exception cref="ApiException">The Api Exception.</exception>
    [HttpDelete]
    [Route("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Ok))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFound))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        try
        {
            // Delete the existing book.
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

    /// <summary>
    /// Get all supported genre
    /// </summary>
    /// <returns>Returns List of string.</returns>
    /// <response code="200">Returns list of supported genre.</response>
    /// <response code="404">Not found error if no supported genre found.</response>
    /// <response code="500">Internal server error.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <exception cref="ApiException">Api Exception</exception>
    [HttpGet]
    [Route("supported-genre")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<string>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFound))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [Produces(MediaTypeNames.Application.Json)]
    public IActionResult GetSupportedGenres()
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
