using System.Net;
using BookBoowingApp.Domain.Common;
using BookBoowingApp.Infrastructure.DB;
using BookBoowingApp.Infrastructure.IRepositories;
using BookBorrowingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookBoowingApp.Infrastructure.RepositoryImplementations;

/// <summary>
/// Represents the implementation of book repository.
/// </summary>
/// <param name="context"></param>
public class BookRepository(ApplicationDbContext context) : IBookRepository
{
    /// <summary>
    /// Represents application db context.
    /// </summary>
    private readonly ApplicationDbContext _context = context;

    /// <summary>
    /// Add new book to database.
    /// </summary>
    /// <param name="book">The new book details.</param>
    /// <returns>Return created book details.</returns>
    /// <exception cref="ApiException">Api Exception.</exception>
    public async Task<Book> CreateBook(Book book)
    {
        try
        {
            var newBook = await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

            return newBook.Entity;
        }
        catch (Exception ex)
        {
            throw new ApiException(HttpStatusCode.InternalServerError, ex);
        }
    }

    /// <summary>
    /// Delete an existing book from database.
    /// </summary>
    /// <param name="book">The existing book details.</param>
    /// <returns>Returns boolean value represent the delete operation result.</returns>
    /// <exception cref="ApiException">The api exception.</exception>
    public async Task<bool> DeleteBook(Book book)
    {
        try
        {
            var deletedBook = _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return deletedBook.Entity.Id == book.Id;
        }
        catch (Exception ex)
        {
            throw new ApiException(HttpStatusCode.InternalServerError, ex);
        }
    }

    /// <summary>
    /// Get list of all books present on database.
    /// </summary>
    /// <returns>Returns list of book.</returns>
    /// <exception cref="ApiException">The Api Exception.</exception>
    public async Task<List<Book>> GetAllBooks()
    {
        try
        {
            var allBooks = await _context.Books.ToListAsync();

            return allBooks;
        }
        catch (Exception ex)
        {
            throw new ApiException(HttpStatusCode.InternalServerError, ex);
        }
    }

    /// <summary>
    /// Get book data by it's id.
    /// </summary>
    /// <param name="id">The book id.</param>
    /// <returns>Returns book details.</returns>
    /// <exception cref="ApiException">The api exception.</exception>
    public async Task<Book?> GetBookById(Guid id)
    {
        try
        {
            var book = await _context.Books.FindAsync(id);

            return book;
        }
        catch (Exception ex)
        {
            throw new ApiException(HttpStatusCode.InternalServerError, ex);
        }
    }

    /// <summary>
    /// Update an existing book details.
    /// </summary>
    /// <param name="book">The updated book details.</param>
    /// <returns>Return updated book details.</returns>
    /// <exception cref="ApiException">The Api Exception.</exception>
    public async Task<Book?> UpdateBook(Book book)
    {
        try
        {
            var updatedBook = _context.Books.Update(book);
            await _context.SaveChangesAsync();

            return updatedBook.Entity;
        }
        catch (Exception ex)
        {
            throw new ApiException(HttpStatusCode.InternalServerError, ex);
        }
    }
}
