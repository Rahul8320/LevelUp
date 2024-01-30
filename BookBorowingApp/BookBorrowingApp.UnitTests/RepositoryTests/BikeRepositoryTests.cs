using BookBoowingApp.Domain.Entities;
using BookBoowingApp.Infrastructure.DB;
using BookBoowingApp.Infrastructure.RepositoryImplementations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BookBorrowingApp.UnitTests.RepositoryTests;

public class BikeRepositoryTests
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
    public async void BikeRepository_Add_ReturnsSuccess()
    {
        // Arrange
        var context = GetDbContext();
        var unitOfWork = new UnitOfWork(context);
        var currentDateTime = DateTime.UtcNow;
        var bike = new Bike()
        {
            Id = Guid.NewGuid(),
            Owner = Guid.NewGuid(),
            Maker = "Royal Enfield",
            Model = "Metor 350 SuperNova",
            RentalPricePerDay = 1000,
            LateFeesPerDay = 1700,
            Description = "Feel the power in your hand!",
            CoverImage = "Cover Image",
            CreatedAt = currentDateTime,
            CurrentBikeStatus = BikeStatus.AvailableForRent,
            FuelCapacity = 15,
            FuelEconomy = 35,
            Images = [],
            IsAvailableForRent = true,
            IsRequestForReturn = false,
            LastUpdated = currentDateTime
        };

        // Act
        unitOfWork.BikeRepository.Add(bike);
        var result = await unitOfWork.Complete();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async void BikeRepository_Delete_ReturnsSuccess()
    {
        // Arrange
        var context = GetDbContext();
        var unitOfWork = new UnitOfWork(context);
        var currentDateTime = DateTime.UtcNow;
        var bike = new Bike()
        {
            Id = Guid.NewGuid(),
            Owner = Guid.NewGuid(),
            Maker = "Royal Enfield",
            Model = "Metor 350 SuperNova",
            RentalPricePerDay = 1000,
            LateFeesPerDay = 1700,
            Description = "Feel the power in your hand!",
            CoverImage = "Cover Image",
            CreatedAt = currentDateTime,
            CurrentBikeStatus = BikeStatus.AvailableForRent,
            FuelCapacity = 15,
            FuelEconomy = 35,
            Images = [],
            IsAvailableForRent = true,
            IsRequestForReturn = false,
            LastUpdated = currentDateTime
        };

        // Act
        unitOfWork.BikeRepository.Add(bike);
        await unitOfWork.Complete();

        unitOfWork.BikeRepository.Delete(bike);
        var result = await unitOfWork.Complete();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async void BikeRepository_Update_ReturnsSuccess()
    {
        // Arrange
        var context = GetDbContext();
        var unitOfWork = new UnitOfWork(context);
        var currentDateTime = DateTime.UtcNow;
        var bike = new Bike()
        {
            Id = Guid.NewGuid(),
            Owner = Guid.NewGuid(),
            Maker = "Royal Enfield",
            Model = "Metor 350 SuperNova",
            RentalPricePerDay = 1000,
            LateFeesPerDay = 1700,
            Description = "Feel the power in your hand!",
            CoverImage = "Cover Image",
            CreatedAt = currentDateTime,
            CurrentBikeStatus = BikeStatus.AvailableForRent,
            FuelCapacity = 15,
            FuelEconomy = 35,
            Images = [],
            IsAvailableForRent = true,
            IsRequestForReturn = false,
            LastUpdated = currentDateTime
        };

        // Act
        unitOfWork.BikeRepository.Add(bike);
        await unitOfWork.Complete();

        bike.CoverImage = "Cover Image Edited!";
        bike.LastUpdated = DateTime.UtcNow;

        unitOfWork.BikeRepository.Update(bike);
        var result = await unitOfWork.Complete();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async void BikeRepository_GetAll_ReturnsListOfBike()
    {
        // Arrange
        var context = GetDbContext();
        var unitOfWork = new UnitOfWork(context);
        var currentDateTime = DateTime.UtcNow;
        var bike = new Bike()
        {
            Id = Guid.NewGuid(),
            Owner = Guid.NewGuid(),
            Maker = "Royal Enfield",
            Model = "Metor 350 SuperNova",
            RentalPricePerDay = 1000,
            LateFeesPerDay = 1700,
            Description = "Feel the power in your hand!",
            CoverImage = "Cover Image",
            CreatedAt = currentDateTime,
            CurrentBikeStatus = BikeStatus.AvailableForRent,
            FuelCapacity = 15,
            FuelEconomy = 35,
            Images = [],
            IsAvailableForRent = true,
            IsRequestForReturn = false,
            LastUpdated = currentDateTime
        };

        // Act
        unitOfWork.BikeRepository.Add(bike);
        await unitOfWork.Complete();

        var result = await unitOfWork.BikeRepository.GetAll();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<Bike>>();
    }

    [Fact]
    public async void BikeRepository_Get_ReturnsBike()
    {
        // Arrange
        var context = GetDbContext();
        var unitOfWork = new UnitOfWork(context);
        var currentDateTime = DateTime.UtcNow;
        var bike = new Bike()
        {
            Id = Guid.NewGuid(),
            Owner = Guid.NewGuid(),
            Maker = "Royal Enfield",
            Model = "Metor 350 SuperNova",
            RentalPricePerDay = 1000,
            LateFeesPerDay = 1700,
            Description = "Feel the power in your hand!",
            CoverImage = "Cover Image",
            CreatedAt = currentDateTime,
            CurrentBikeStatus = BikeStatus.AvailableForRent,
            FuelCapacity = 15,
            FuelEconomy = 35,
            Images = [],
            IsAvailableForRent = true,
            IsRequestForReturn = false,
            LastUpdated = currentDateTime
        };

        // Act
        unitOfWork.BikeRepository.Add(bike);
        await unitOfWork.Complete();

        var result = await unitOfWork.BikeRepository.Get(bike.Id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Bike>();
    }

    [Fact]
    public async void BikeRepository_Get_ReturnsNull()
    {
        // Arrange
        var context = GetDbContext();
        var unitOfWork = new UnitOfWork(context);

        // Act
        var result = await unitOfWork.BikeRepository.Get(Guid.NewGuid());

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async void BikeRepository_Find_ReturnsBike()
    {
        // Arrange
        var context = GetDbContext();
        var unitOfWork = new UnitOfWork(context);
        var currentDateTime = DateTime.UtcNow;
        var bike = new Bike()
        {
            Id = Guid.NewGuid(),
            Owner = Guid.NewGuid(),
            Maker = "Royal Enfield",
            Model = "Metor 350 SuperNova",
            RentalPricePerDay = 1000,
            LateFeesPerDay = 1700,
            Description = "Feel the power in your hand!",
            CoverImage = "Cover Image",
            CreatedAt = currentDateTime,
            CurrentBikeStatus = BikeStatus.AvailableForRent,
            FuelCapacity = 15,
            FuelEconomy = 35,
            Images = [],
            IsAvailableForRent = true,
            IsRequestForReturn = false,
            LastUpdated = currentDateTime
        };

        // Act
        unitOfWork.BikeRepository.Add(bike);
        await unitOfWork.Complete();

        var result = await unitOfWork.BikeRepository.Find(u => u.IsAvailableForRent == true);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Bike>();
    }

    [Fact]
    public async void BikeRepository_Find_ReturnsNull()
    {
        // Arrange
        var context = GetDbContext();
        var unitOfWork = new UnitOfWork(context);

        // Act
        var result = await unitOfWork.BikeRepository.Find(u => u.IsAvailableForRent == true);

        // Assert
        result.Should().BeNull();
    }
}
