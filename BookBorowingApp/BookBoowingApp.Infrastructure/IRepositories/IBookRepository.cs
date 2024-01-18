using BookBorrowingApp.Domain.Entities;

namespace BookBoowingApp.Infrastructure.IRepositories;

public interface IBookRepository
{
    public List<Book> GetAllBooks();
    public Book? GetBookById(Guid id);
    public Book CreateBook();
    public Book? UpdateBook();
    public bool DeleteBook();
}
