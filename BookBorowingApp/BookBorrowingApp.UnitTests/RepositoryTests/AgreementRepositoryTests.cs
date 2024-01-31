using BookBoowingApp.Domain.Entities;
using BookBoowingApp.Infrastructure.DB;
using BookBoowingApp.Infrastructure.RepositoryImplementations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BookBorrowingApp.UnitTests.RepositoryTests;

public class AgreementRepositoryTests
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
    public async void AgreementRepository_Add_ReturnsSuccess()
    {
        // Arrange
        var context = GetDbContext();
        var unitOfWork = new UnitOfWork(context);
        var agreement = new Agreement()
        {
            BikeId = Guid.NewGuid(),
            BikeOwnerId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Duration = 2,
            EndDate = DateTime.Today,
            Id = Guid.NewGuid(),
            IsAcceptedByUser = false,
            LastUpdated = DateTime.UtcNow,
            StartDate = DateTime.Today,
            TotalCost = 2000,
            UserId = Guid.NewGuid(),
        };

        // Act
        unitOfWork.AgreementRepository.Add(agreement);
        var result = await unitOfWork.Complete();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async void AgreementRepository_Delete_ReturnsSuccess()
    {
        // Arrange
        var context = GetDbContext();
        var unitOfWork = new UnitOfWork(context);
        var agreement = new Agreement()
        {
            BikeId = Guid.NewGuid(),
            BikeOwnerId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Duration = 2,
            EndDate = DateTime.Today,
            Id = Guid.NewGuid(),
            IsAcceptedByUser = false,
            LastUpdated = DateTime.UtcNow,
            StartDate = DateTime.Today,
            TotalCost = 2000,
            UserId = Guid.NewGuid(),
        };

        // Act
        unitOfWork.AgreementRepository.Add(agreement);
        await unitOfWork.Complete();

        unitOfWork.AgreementRepository.Delete(agreement);
        var result = await unitOfWork.Complete();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async void AgreementRepository_Update_ReturnsSuccess()
    {
        // Arrange
        var context = GetDbContext();
        var unitOfWork = new UnitOfWork(context);
        var agreement = new Agreement()
        {
            BikeId = Guid.NewGuid(),
            BikeOwnerId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Duration = 2,
            EndDate = DateTime.Today,
            Id = Guid.NewGuid(),
            IsAcceptedByUser = false,
            LastUpdated = DateTime.UtcNow,
            StartDate = DateTime.Today,
            TotalCost = 2000,
            UserId = Guid.NewGuid(),
        };

        // Act
        unitOfWork.AgreementRepository.Add(agreement);
        await unitOfWork.Complete();

        agreement.Duration = 1;
        agreement.LastUpdated = DateTime.UtcNow;

        unitOfWork.AgreementRepository.Update(agreement);
        var result = await unitOfWork.Complete();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async void AgreementRepository_GetAll_ReturnsListOfAgreement()
    {
        // Arrange
        var context = GetDbContext();
        var unitOfWork = new UnitOfWork(context);
        var agreement = new Agreement()
        {
            BikeId = Guid.NewGuid(),
            BikeOwnerId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Duration = 2,
            EndDate = DateTime.Today,
            Id = Guid.NewGuid(),
            IsAcceptedByUser = false,
            LastUpdated = DateTime.UtcNow,
            StartDate = DateTime.Today,
            TotalCost = 2000,
            UserId = Guid.NewGuid(),
        };

        // Act
        unitOfWork.AgreementRepository.Add(agreement);
        await unitOfWork.Complete();

        var result = await unitOfWork.AgreementRepository.GetAll();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<Agreement>>();
    }

    [Fact]
    public async void AgreementRepository_Get_ReturnsAgreement()
    {
        // Arrange
        var context = GetDbContext();
        var unitOfWork = new UnitOfWork(context);
        var agreement = new Agreement()
        {
            BikeId = Guid.NewGuid(),
            BikeOwnerId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Duration = 2,
            EndDate = DateTime.Today,
            Id = Guid.NewGuid(),
            IsAcceptedByUser = false,
            LastUpdated = DateTime.UtcNow,
            StartDate = DateTime.Today,
            TotalCost = 2000,
            UserId = Guid.NewGuid(),
        };

        // Act
        unitOfWork.AgreementRepository.Add(agreement);
        await unitOfWork.Complete();

        var result = await unitOfWork.AgreementRepository.Get(agreement.Id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Agreement>();
    }

    [Fact]
    public async void AgreementRepository_Get_ReturnsNull()
    {
        // Arrange
        var context = GetDbContext();
        var unitOfWork = new UnitOfWork(context);

        // Act
        var result = await unitOfWork.AgreementRepository.Get(Guid.NewGuid());

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async void AgreementRepository_Find_ReturnsAgreement()
    {
        // Arrange
        var context = GetDbContext();
        var unitOfWork = new UnitOfWork(context);
        var agreement = new Agreement()
        {
            BikeId = Guid.NewGuid(),
            BikeOwnerId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Duration = 2,
            EndDate = DateTime.Today,
            Id = Guid.NewGuid(),
            IsAcceptedByUser = false,
            LastUpdated = DateTime.UtcNow,
            StartDate = DateTime.Today,
            TotalCost = 2000,
            UserId = Guid.NewGuid(),
        };

        // Act
        unitOfWork.AgreementRepository.Add(agreement);
        await unitOfWork.Complete();

        var result = await unitOfWork.AgreementRepository.Find(u => u.Duration == 2);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Agreement>();
    }

    [Fact]
    public async void AgreementRepository_Find_ReturnsNull()
    {
        // Arrange
        var context = GetDbContext();
        var unitOfWork = new UnitOfWork(context);
        var agreement = new Agreement()
        {
            BikeId = Guid.NewGuid(),
            BikeOwnerId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Duration = 2,
            EndDate = DateTime.Today,
            Id = Guid.NewGuid(),
            IsAcceptedByUser = false,
            LastUpdated = DateTime.UtcNow,
            StartDate = DateTime.Today,
            TotalCost = 2000,
            UserId = Guid.NewGuid(),
        };

        // Act
        unitOfWork.AgreementRepository.Add(agreement);
        await unitOfWork.Complete();

        var result = await unitOfWork.AgreementRepository.Find(u => u.Duration == 4);

        // Assert
        result.Should().BeNull();
    }

}
