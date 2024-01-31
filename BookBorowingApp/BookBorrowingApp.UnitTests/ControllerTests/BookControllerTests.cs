using System.Net;
using BookBoowingApp.Domain.Common;
using BookBoowingApp.Service.IServices;
using BookBoowingApp.Service.Models;
using BookBorrowingApp.Application.Controllers;
using BookBorrowingApp.Domain.Entities;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace BookBorrowingApp.UnitTests.ControllerTests;

public class BookControllerTests
{
    private readonly IBookService _bookService;
    private readonly BookController _bookController;

    public BookControllerTests()
    {
        // Dependencies
        _bookService = A.Fake<IBookService>();

        // SUT
        _bookController = new BookController(_bookService);
    }

    [Fact]
    public async void BookController_GetAllAvailableBooks_ReturnsOk()
    {
        // Arrange
        var allBooks = new ServiceResult<List<Book>>(HttpStatusCode.OK, [new Book()]);
        A.CallTo(() => _bookService.GetAllAvailableBooks()).Returns(allBooks);

        // Act
        var result = await _bookController.GetAllAvailableBooks();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async void BookController_GetAllAvailableBooks_ReturnsNotFound()
    {
        // Arrange
        var allBooks = A.Fake<ServiceResult<List<Book>>>();
        A.CallTo(() => _bookService.GetAllAvailableBooks()).Returns(allBooks);

        // Act
        var result = await _bookController.GetAllAvailableBooks();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async void BookController_BookDetails_ReturnsOk()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var bookDetails = A.Fake<ServiceResult<Book>>();
        A.CallTo(() => _bookService.GetBookDetails(bookId))!.Returns(bookDetails);

        // Act
        var result = await _bookController.BookDetails(bookId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async void BookController_BookDetails_ReturnNotFound()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var bookDetails = new ServiceResult<Book>(HttpStatusCode.NotFound);
        A.CallTo(() => _bookService.GetBookDetails(bookId))!.Returns(bookDetails);

        // Act
        var result = await _bookController.BookDetails(bookId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async void BookController_CreateNewBook_ReturnsCreated()
    {
        // Arrange
        var addBookModel = A.Fake<AddBookModel>();
        var book = A.Fake<Book>();
        var createdBook = new ServiceResult<Book>(HttpStatusCode.Created, book);
        A.CallTo(() => _bookService.AddNewBook(addBookModel)).Returns(createdBook);

        // Act
        var result = await _bookController.CreateNewBook(addBookModel);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ObjectResult>();
    }

    [Fact]
    public async void BookController_CreateNewBook_ReturnsBadRequest()
    {
        // Arrange
        var addBookModel = new AddBookModel();
        var book = A.Fake<Book>();
        var createdBook = new ServiceResult<Book>(HttpStatusCode.Created, book);
        A.CallTo(() => _bookService.AddNewBook(addBookModel)).Returns(createdBook);

        // Act
        var result = await _bookController.CreateNewBook(addBookModel);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ObjectResult>();
    }
}