using System.Net;
using BookBoowingApp.Domain.Common;
using BookBoowingApp.Service.IServices;
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
    public async void BookController_GetAllAvailableBooks_ReturnsSuccess()
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
    public async void BookController_BookDetails_ReturnSuccess()
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
}
