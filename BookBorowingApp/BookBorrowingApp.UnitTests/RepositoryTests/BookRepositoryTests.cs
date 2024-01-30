using BookBoowingApp.Infrastructure.DB;
using BookBoowingApp.Infrastructure.RepositoryImplementations;
using BookBorrowingApp.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BookBorrowingApp.UnitTests.RepositoryTests;

public class BookRepositoryTests
{
    // Dependency
    private ApplicationDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new ApplicationDbContext(options);
        context.Database.EnsureCreated();
        return context;
    }

    [Fact]
    public async void BookRepository_CreateBook_ReturnsCreatedBook()
    {
        // Arrange
        var context = GetDbContext();
        var bookRepository = new BookRepository(context);
        var currentDateTime = DateTime.UtcNow;
        var book = new Book()
        {
            Id = Guid.NewGuid(),
            Name = "Deep Work",
            Author = "Cal Newport",
            Description = "Rules for focused success in a distracted world.",
            CoverImage = "Cover Image",
            Genre = "Self-Help",
            Images = [],
            Currently_Borrowed_By_User_Id = null,
            Is_Book_Available = true,
            Lent_By_User_id = Guid.NewGuid(),
            Rating = 5,
            CreatedAt = DateTime.UtcNow,
            LastUpdated = DateTime.UtcNow,
        };

        // Act
        var result = await bookRepository.CreateBook(book);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Book>();
        result.Id.Should().Be(book.Id);
        result.Name.Should().Be(book.Name);
        result.Author.Should().Be(book.Author);
        result.Description.Should().Be(book.Description);
        result.CoverImage.Should().Be(book.CoverImage);
        result.Genre.Should().Be(book.Genre);
        result.Images.Should().HaveCount(0);
        result.Currently_Borrowed_By_User_Id.Should().BeNull();
        result.Is_Book_Available.Should().Be(book.Is_Book_Available);
        result.Lent_By_User_id.Should().Be(book.Lent_By_User_id);
        result.Rating.Should().Be(book.Rating);
        result.CreatedAt.Should().BeAfter(currentDateTime);
        result.LastUpdated.Should().BeAfter(currentDateTime);
    }

    [Fact]
    public async void BookRepository_GetBookById_ReturnsBook()
    {
        // Arrange
        var context = GetDbContext();
        var bookRepository = new BookRepository(context);
        var currentDateTime = DateTime.UtcNow;
        var book = new Book()
        {
            Id = Guid.NewGuid(),
            Name = "Deep Work",
            Author = "Cal Newport",
            Description = "Rules for focused success in a distracted world.",
            CoverImage = "Cover Image",
            Genre = "Self-Help",
            Images = [],
            Currently_Borrowed_By_User_Id = null,
            Is_Book_Available = true,
            Lent_By_User_id = Guid.NewGuid(),
            Rating = 5,
            CreatedAt = DateTime.UtcNow,
            LastUpdated = DateTime.UtcNow,
        };

        // Act
        await bookRepository.CreateBook(book);
        var result = await bookRepository.GetBookById(book.Id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Book>();
        result!.Id.Should().Be(book.Id);
        result.Name.Should().Be(book.Name);
        result.Author.Should().Be(book.Author);
        result.Description.Should().Be(book.Description);
        result.CoverImage.Should().Be(book.CoverImage);
        result.Genre.Should().Be(book.Genre);
        result.Images.Should().HaveCount(0);
        result.Currently_Borrowed_By_User_Id.Should().BeNull();
        result.Is_Book_Available.Should().Be(book.Is_Book_Available);
        result.Lent_By_User_id.Should().Be(book.Lent_By_User_id);
        result.Rating.Should().Be(book.Rating);
        result.CreatedAt.Should().BeAfter(currentDateTime);
        result.LastUpdated.Should().BeAfter(currentDateTime);
    }

    [Fact]
    public async void BookRepository_GetAllBooks_ReturnsListOfBook()
    {
        // Arrange
        var context = GetDbContext();
        var bookRepository = new BookRepository(context);

        // Act
        var result = await bookRepository.GetAllBooks();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<Book>>();
    }

    [Fact]
    public async void BookRepository_UpdateBook_ReturnsUpdatedBook()
    {
        // Arrange
        var context = GetDbContext();
        var bookRepository = new BookRepository(context);
        context.Books.AsNoTracking();
        var book = new Book()
        {
            Id = Guid.NewGuid(),
            Name = "Deep Work",
            Author = "Cal Newport",
            Description = "Rules for focused success in a distracted world.",
            CoverImage = "Cover Image",
            Genre = "Self-Help",
            Images = [],
            Currently_Borrowed_By_User_Id = null,
            Is_Book_Available = true,
            Lent_By_User_id = Guid.NewGuid(),
            Rating = 5,
            CreatedAt = DateTime.UtcNow,
            LastUpdated = DateTime.UtcNow,
        };

        var currentDateTime = DateTime.UtcNow;

        // Act
        var createdBook = await bookRepository.CreateBook(book);
        createdBook.Name = "Deep Work Edited";
        createdBook.CoverImage = "Edited";
        createdBook.Rating = 4;
        createdBook.LastUpdated = DateTime.UtcNow;
        var result = await bookRepository.UpdateBook(createdBook);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Book>();
        result!.Id.Should().Be(createdBook.Id);
        result.Name.Should().Be(createdBook.Name);
        result.Author.Should().Be(createdBook.Author);
        result.Description.Should().Be(createdBook.Description);
        result.CoverImage.Should().Be(createdBook.CoverImage);
        result.Genre.Should().Be(createdBook.Genre);
        result.Images.Should().HaveCount(0);
        result.Currently_Borrowed_By_User_Id.Should().BeNull();
        result.Is_Book_Available.Should().Be(createdBook.Is_Book_Available);
        result.Lent_By_User_id.Should().Be(createdBook.Lent_By_User_id);
        result.Rating.Should().Be(createdBook.Rating);
        result.CreatedAt.Should().BeBefore(currentDateTime);
        result.LastUpdated.Should().BeAfter(currentDateTime);
    }

    [Fact]
    public async void BookRepository_DeleteBook_ReturnsTrue()
    {
        // Arrange
        var context = GetDbContext();
        var bookRepository = new BookRepository(context);
        var book = new Book()
        {
            Id = Guid.NewGuid(),
            Name = "Deep Work",
            Author = "Cal Newport",
            Description = "Rules for focused success in a distracted world.",
            CoverImage = "Cover Image",
            Genre = "Self-Help",
            Images = [],
            Currently_Borrowed_By_User_Id = null,
            Is_Book_Available = true,
            Lent_By_User_id = Guid.NewGuid(),
            Rating = 5,
            CreatedAt = DateTime.UtcNow,
            LastUpdated = DateTime.UtcNow,
        };

        // Act
        await bookRepository.CreateBook(book);
        var result = await bookRepository.DeleteBook(book);

        // Assert
        result.Should().BeTrue();
    }

}
