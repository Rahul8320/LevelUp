using System.Net;
using System.Security.Claims;
using BookBoowingApp.Domain.Common;
using BookBoowingApp.Infrastructure.IRepositories;
using BookBoowingApp.Service.Common;
using BookBoowingApp.Service.DTOs;
using BookBoowingApp.Service.IServices;
using BookBoowingApp.Service.Models;
using BookBorrowingApp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace BookBoowingApp.Service.ServiceImplementations;

/// <summary>
/// Book service class for maintains all operations.
/// </summary>
/// <param name="bookRepository">The book repository interface.</param>
public class BookService(IBookRepository bookRepository, IOptions<AppSettings> appSettings, IAuthService authService) : IBookService
{
    /// <summary>
    /// Book repository for maintaining books in database.
    /// </summary>
    private readonly IBookRepository _bookRepository = bookRepository;
    /// <summary>
    /// Represents application settings.
    /// </summary>
    private readonly IOptions<AppSettings> _appSettings = appSettings;
    /// <summary>
    /// Represents the auth service interface.
    /// </summary>
    private readonly IAuthService _authService = authService;

    /// <summary>
    /// Add new book into database.
    /// </summary>
    /// <param name="bookModel">The new book details.</param>
    /// <returns>Returns the newly added book details.</returns>
    /// <exception cref="ApiException">Api Exception</exception>
    public async Task<ServiceResult<Book>> AddNewBook(AddBookModel bookModel)
    {
        try
        {
            // Fetch authenticated user data.
            var userData = _authService.GetAuthenticatedUserData();

            // create book entity
            var bookEntity = new Book()
            {
                Id = Guid.NewGuid(),
                Name = bookModel.Name,
                Description = bookModel.Description,
                Rating = 0,
                Author = bookModel.Author,
                CoverImage = bookModel.CoverImage,
                Genre = bookModel.Genre,
                Is_Book_Available = true,
                Lent_By_User_id = Guid.Parse(userData.UserId!),
                Currently_Borrowed_By_User_Id = null,
                Images = bookModel.Images,
                CreatedAt = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            };

            // add book to database
            var addedBook = await _bookRepository.CreateBook(bookEntity)
                ?? throw new ApiException(
                    HttpStatusCode.InternalServerError,
                    new Exception("Book created failed!"
                )
            );

            // Returns the newly created book data with 201 status code.
            return new ServiceResult<Book>(HttpStatusCode.Created, addedBook);
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
    /// Delete single book from database.
    /// </summary>
    /// <param name="id">The requested book id.</param>
    /// <returns>Returns the service result indicating the success or failure of the delete operation.</returns>
    /// <exception cref="ApiException">The Api Exception.</exception>
    public async Task<ServiceResult> DeleteBookById(Guid id)
    {
        try
        {
            // Fetch existing book data.
            var existingBook = await _bookRepository.GetBookById(id);

            // Check for book exists or not.
            if (existingBook == null)
            {
                return new ServiceResult(HttpStatusCode.NotFound);
            }

            // Delete the book from the DB.
            var result = await _bookRepository.DeleteBook(existingBook);

            if (result == false)
            {
                throw new ApiException(HttpStatusCode.InternalServerError, new Exception($"Book with id: {id} deleted failed!"));
            }

            // Return the service result with 200 status code.
            return new ServiceResult(HttpStatusCode.OK);
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
    /// Get all available books details.
    /// </summary>
    /// <returns>Returns list of books.</returns>
    /// <exception cref="ApiException">Api Exception.</exception>
    public async Task<ServiceResult<List<Book>>> GetAllAvailableBooks()
    {
        try
        {
            // Fetch all books from database.
            var allBooks = await _bookRepository.GetAllBooks();

            // Filter the list for only available books.
            var availableBooks = allBooks.Where(item => item.Is_Book_Available == true).ToList();

            // Return the list of available books.
            return new ServiceResult<List<Book>>(HttpStatusCode.OK, availableBooks);
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
    /// <param name="id">The requested book id.</param>
    /// <returns>Returns the book details.</returns>
    /// <exception cref="ApiException">Api Exception.</exception>
    public async Task<ServiceResult<Book?>> GetBookDetails(Guid id)
    {
        try
        {
            var bookDetails = await _bookRepository.GetBookById(id);

            if (bookDetails == null)
            {
                return new ServiceResult<Book?>(HttpStatusCode.NotFound);
            }

            return new ServiceResult<Book?>(HttpStatusCode.OK, bookDetails);
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
    /// Update book details.
    /// </summary>
    /// <param name="id">The id of the book</param>
    /// <param name="bookModel">The updated details of the book.</param>
    /// <returns>Returns the updated book details.</returns>
    /// <exception cref="ApiException">The Api Exception</exception>
    public async Task<ServiceResult<Book>> UpdateBookById(Guid id, AddBookModel bookModel)
    {
        try
        {
            // Fetch existing book data.
            var existingBook = await _bookRepository.GetBookById(id);

            // Check for book exists or not.
            if (existingBook == null)
            {
                return new ServiceResult<Book>(HttpStatusCode.NotFound);
            }

            // update book entity
            existingBook.Name = bookModel.Name ?? existingBook.Name;
            existingBook.Description = bookModel.Description ?? existingBook.Description;
            existingBook.Author = bookModel.Author ?? existingBook.Author;
            existingBook.CoverImage = bookModel.CoverImage ?? existingBook.CoverImage;
            existingBook.Genre = bookModel.Genre ?? existingBook.Genre;
            existingBook.Images = bookModel.Images ?? existingBook.Images;
            existingBook.LastUpdated = DateTime.UtcNow;

            // Update the book on database.
            var updatedBook = await _bookRepository.UpdateBook(existingBook)
                ?? throw new ApiException(HttpStatusCode.InternalServerError,
                new Exception($"Book with id: {id} updated failed!"));

            return new ServiceResult<Book>(HttpStatusCode.OK, updatedBook);
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
    /// Get all supported genres.
    /// </summary>
    /// <returns>Returns list of all genres.</returns>
    /// <exception cref="ApiException">Api Exception.</exception>
    public ServiceResult<List<string>> GetSupportedGenre()
    {
        try
        {
            var allGenres = _appSettings.Value.SupportedGenres;
            return new ServiceResult<List<string>>(HttpStatusCode.OK, allGenres);
        }
        catch (Exception)
        {
            throw new ApiException(HttpStatusCode.InternalServerError, new Exception("Can not get supported genres."));
        }
    }

}
