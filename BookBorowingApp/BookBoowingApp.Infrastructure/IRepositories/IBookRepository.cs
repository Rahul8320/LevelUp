using BookBorrowingApp.Domain.Entities;

namespace BookBoowingApp.Infrastructure.IRepositories;

/// <summary>
/// Represents the interface for book repository.
/// </summary>
public interface IBookRepository
{
    /// <summary>
    /// Get list of all books present on database.
    /// </summary>
    /// <returns>Returns list of book.</returns>
    public Task<List<Book>> GetAllBooks();

    /// <summary>
    /// Get book data by it's id.
    /// </summary>
    /// <param name="id">The book id.</param>
    /// <returns>Returns book details.</returns>
    public Task<Book?> GetBookById(Guid id);

    /// <summary>
    /// Add new book to database.
    /// </summary>
    /// <param name="book">The new book details.</param>
    /// <returns>Return created book details.</returns>
    public Task<Book> CreateBook(Book book);

    /// <summary>
    /// Update an existing book details.
    /// </summary>
    /// <param name="book">The updated book details.</param>
    /// <returns>Return updated book details.</returns>
    public Task<Book?> UpdateBook(Book book);

    /// <summary>
    /// Delete an existing book from database.
    /// </summary>
    /// <param name="book">The existing book details.</param>
    /// <returns>Returns boolean value represent the delete operation result.</returns>
    public Task<bool> DeleteBook(Book book);
}
