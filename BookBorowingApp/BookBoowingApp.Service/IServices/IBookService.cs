using BookBoowingApp.Domain.Common;
using BookBoowingApp.Service.Models;
using BookBorrowingApp.Domain.Entities;

namespace BookBoowingApp.Service.IServices;

public interface IBookService
{
    public Task<ServiceResult<List<Book>>> GetAllAvailableBooks();
    public Task<ServiceResult<Book?>> GetBookDetails(Guid id);
    public Task<ServiceResult<Book>> AddNewBook(AddBookModel bookModel);
    public Task<ServiceResult<Book>> UpdateBookById(Guid id, AddBookModel bookModel);
    public Task<ServiceResult> DeleteBookById(Guid id);
}
