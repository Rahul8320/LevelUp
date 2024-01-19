using BookBorrowingApp.Domain.Entities;

namespace BookBoowingApp.Infrastructure.IRepositories;

public interface IBookRepository
{
    public Task<List<Book>> GetAllBooks();
    public Task<Book?> GetBookById(Guid id);
    public Task<Book> CreateBook(Book book);
    public Task<Book?> UpdateBook(Book book);
    public Task<bool> DeleteBook(Book book);
}
