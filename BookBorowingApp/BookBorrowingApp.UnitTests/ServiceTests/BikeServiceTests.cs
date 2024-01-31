using System.Net;
using BookBoowingApp.Domain.Common;
using BookBoowingApp.Domain.Entities;
using BookBoowingApp.Infrastructure.IRepositories;
using BookBoowingApp.Service.Models;
using BookBoowingApp.Service.ServiceImplementations;
using FakeItEasy;
using FluentAssertions;

namespace BookBorrowingApp.UnitTests.ServiceTests;

public class BikeServiceTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly BikeService _bikeService;
    public BikeServiceTests()
    {
        // Dependency
        _unitOfWork = A.Fake<IUnitOfWork>();

        // SUT
        _bikeService = new BikeService(_unitOfWork);
    }

    [Fact]
    public async void BikeService_CreateNewBike_ReturnsSuccess()
    {
        // Arrange
        var addBikeModel = A.Fake<AddBikeModel>();
        var userId = Guid.NewGuid();

        A.CallTo(() => _unitOfWork.Complete()).Returns(true);

        // Act
        var result = await _bikeService.CreateNewBike(addBikeModel, userId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult>();
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        result.Message.Should().BeNull();
        result.ValidationError.Should().BeNull();

        A.CallTo(() => _unitOfWork.Complete()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async void BikeService_DeleteBike_ReturnsOk()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var bikeId = Guid.NewGuid();
        var bike = A.Fake<Bike>();
        bike.Owner = userId;

        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).Returns(bike);
        A.CallTo(() => _unitOfWork.BikeRepository.Delete(bike));
        A.CallTo(() => _unitOfWork.Complete()).Returns(true);

        // Act
        var result = await _bikeService.DeleteBike(bikeId, userId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult>();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Message.Should().BeNull();
        result.ValidationError.Should().BeNull();

        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.BikeRepository.Delete(bike)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.Complete()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async void BikeService_DeleteBike_ReturnsForbidden()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var bikeId = Guid.NewGuid();
        var bike = A.Fake<Bike>();

        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).Returns(bike);
        A.CallTo(() => _unitOfWork.BikeRepository.Delete(bike));
        A.CallTo(() => _unitOfWork.Complete()).Returns(true);

        // Act
        var result = await _bikeService.DeleteBike(bikeId, userId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult>();
        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        result.Message.Should().Be("You don't have access to delete this resource.");
        result.ValidationError.Should().BeNull();

        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.BikeRepository.Delete(bike)).MustNotHaveHappened();
        A.CallTo(() => _unitOfWork.Complete()).MustNotHaveHappened();
    }

    [Fact]
    public async void BikeService_DeleteBike_ReturnsNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var bikeId = Guid.Empty;
        var bike = A.Fake<Bike>();

        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId))!.Returns(Task.FromResult<Bike>(null!));
        A.CallTo(() => _unitOfWork.BikeRepository.Delete(bike));
        A.CallTo(() => _unitOfWork.Complete()).Returns(true);

        // Act
        var result = await _bikeService.DeleteBike(bikeId, userId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult>();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        result.Message.Should().Be($"Bike with id: {bikeId} can not be found!");
        result.ValidationError.Should().BeNull();

        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.BikeRepository.Delete(bike)).MustNotHaveHappened();
        A.CallTo(() => _unitOfWork.Complete()).MustNotHaveHappened();
    }
}
