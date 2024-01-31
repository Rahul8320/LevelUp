using BookBoowingApp.Domain.Entities;
using BookBoowingApp.Infrastructure.DB;
using BookBoowingApp.Infrastructure.RepositoryImplementations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BookBorrowingApp.UnitTests.RepositoryTests;

public class BikeRatingRepositoryTests
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
    public async void BikeRatingRepository_Add_ReturnsSuccess()
    {
        // Arrange
        var context = GetDbContext();
        var unitOfWork = new UnitOfWork(context);
        var bikeRating = new BikeRating()
        {
            BikeId = Guid.NewGuid(),
            Id = Guid.NewGuid(),
            LastUpdated = DateTime.UtcNow,
            Rating = 4,
            Review = "Best for hill",
            UserId = Guid.NewGuid()
        };

        // Act
        unitOfWork.BikeRatingRepository.Add(bikeRating);
        var result = await unitOfWork.Complete();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async void BikeRatingRepository_Delete_ReturnsSuccess()
    {
        // Arrange
        var context = GetDbContext();
        var unitOfWork = new UnitOfWork(context);
        var bikeRating = new BikeRating()
        {
            BikeId = Guid.NewGuid(),
            Id = Guid.NewGuid(),
            LastUpdated = DateTime.UtcNow,
            Rating = 4,
            Review = "Best for hill",
            UserId = Guid.NewGuid()
        };

        // Act
        unitOfWork.BikeRatingRepository.Add(bikeRating);
        await unitOfWork.Complete();

        unitOfWork.BikeRatingRepository.Delete(bikeRating);
        var result = await unitOfWork.Complete();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async void BikeRatingRepository_Update_ReturnsSuccess()
    {
        // Arrange
        var context = GetDbContext();
        var unitOfWork = new UnitOfWork(context);
        var bikeRating = new BikeRating()
        {
            BikeId = Guid.NewGuid(),
            Id = Guid.NewGuid(),
            LastUpdated = DateTime.UtcNow,
            Rating = 4,
            Review = "Best for hill",
            UserId = Guid.NewGuid()
        };

        // Act
        unitOfWork.BikeRatingRepository.Add(bikeRating);
        await unitOfWork.Complete();

        bikeRating.Rating = 5;
        bikeRating.Review = "Best for mountain ride";

        unitOfWork.BikeRatingRepository.Update(bikeRating);
        var result = await unitOfWork.Complete();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async void BikeRatingRepository_GetAll_ReturnsListOfBikeRating()
    {
        // Arrange
        var context = GetDbContext();
        var unitOfWork = new UnitOfWork(context);
        var bikeRating = new BikeRating()
        {
            BikeId = Guid.NewGuid(),
            Id = Guid.NewGuid(),
            LastUpdated = DateTime.UtcNow,
            Rating = 4,
            Review = "Best for hill",
            UserId = Guid.NewGuid()
        };

        // Act
        unitOfWork.BikeRatingRepository.Add(bikeRating);
        await unitOfWork.Complete();

        var result = await unitOfWork.BikeRatingRepository.GetAll();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<BikeRating>>();
    }

    [Fact]
    public async void BikeRatingRepository_Get_ReturnsBikeRating()
    {
        // Arrange
        var context = GetDbContext();
        var unitOfWork = new UnitOfWork(context);
        var bikeRating = new BikeRating()
        {
            BikeId = Guid.NewGuid(),
            Id = Guid.NewGuid(),
            LastUpdated = DateTime.UtcNow,
            Rating = 4,
            Review = "Best for hill",
            UserId = Guid.NewGuid()
        };

        // Act
        unitOfWork.BikeRatingRepository.Add(bikeRating);
        await unitOfWork.Complete();

        var result = await unitOfWork.BikeRatingRepository.Get(bikeRating.Id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<BikeRating>();
    }

    [Fact]
    public async void BikeRatingRepository_Get_ReturnsNull()
    {
        // Arrange
        var context = GetDbContext();
        var unitOfWork = new UnitOfWork(context);

        // Act
        var result = await unitOfWork.BikeRatingRepository.Get(Guid.NewGuid());

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async void BikeRatingRepository_Find_ReturnsBikeRating()
    {
        // Arrange
        var context = GetDbContext();
        var unitOfWork = new UnitOfWork(context);
        var bikeRating = new BikeRating()
        {
            BikeId = Guid.NewGuid(),
            Id = Guid.NewGuid(),
            LastUpdated = DateTime.UtcNow,
            Rating = 4,
            Review = "Best for hill",
            UserId = Guid.NewGuid()
        };

        // Act
        unitOfWork.BikeRatingRepository.Add(bikeRating);
        await unitOfWork.Complete();

        var result = await unitOfWork.BikeRatingRepository.Find(u => u.Rating == 4);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<BikeRating>();
    }

    [Fact]
    public async void BikeRatingRepository_Find_ReturnsNull()
    {
        // Arrange
        var context = GetDbContext();
        var unitOfWork = new UnitOfWork(context);

        // Act
        var result = await unitOfWork.BikeRatingRepository.Find(u => u.Rating == 4);

        // Assert
        result.Should().BeNull();
    }
}
