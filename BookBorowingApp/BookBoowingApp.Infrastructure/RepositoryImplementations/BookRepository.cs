using System.Net;
using BookBoowingApp.Domain.Common;
using BookBoowingApp.Infrastructure.DB;
using BookBoowingApp.Infrastructure.IRepositories;
using BookBorrowingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookBoowingApp.Infrastructure.RepositoryImplementations;

public class BookRepository(ApplicationDbContext context) : IBookRepository
{
    private readonly ApplicationDbContext _context = context;

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
