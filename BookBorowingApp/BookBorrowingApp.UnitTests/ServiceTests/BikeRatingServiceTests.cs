using System.Net;
using BookBoowingApp.Domain.Common;
using BookBoowingApp.Domain.Entities;
using BookBoowingApp.Infrastructure.IRepositories;
using BookBoowingApp.Service.DTOs;
using BookBoowingApp.Service.Models;
using BookBoowingApp.Service.ServiceImplementations;
using FakeItEasy;
using FluentAssertions;

namespace BookBorrowingApp.UnitTests.ServiceTests;

public class BikeRatingServiceTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly BikeRatingService _bikeRatingService;
    public BikeRatingServiceTests()
    {
        // Dependency
        _unitOfWork = A.Fake<IUnitOfWork>();

        // SUT
        _bikeRatingService = new BikeRatingService(_unitOfWork);
    }

    [Fact]
    public async void BikeRatingService_AddRating_ReturnsCreated()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var bikeId = Guid.NewGuid();
        var addBikeRatingModel = A.Fake<AddBikeRatingModel>();
        addBikeRatingModel.BikeId = bikeId;
        var bike = A.Fake<Bike>();

        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).Returns(bike);
        A.CallTo(() => _unitOfWork.Complete()).Returns(true);

        // Act
        var result = await _bikeRatingService.AddRating(addBikeRatingModel, userId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult>();
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        result.Message.Should().BeNull();
        result.ValidationError.Should().BeNull();

        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.Complete()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async void BikeRatingService_AddRating_ReturnsBadRequest()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var bikeId = Guid.NewGuid();
        var addBikeRatingModel = A.Fake<AddBikeRatingModel>();
        addBikeRatingModel.BikeId = bikeId;

        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId))!.Returns(Task.FromResult<Bike>(null!));
        A.CallTo(() => _unitOfWork.Complete()).Returns(true);

        // Act
        var result = await _bikeRatingService.AddRating(addBikeRatingModel, userId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult>();
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.Message.Should().BeNull();
        result.ValidationError.Should().NotBeNull();
        result.ValidationError.Code.Should().Be("InvalidBikeId");
        result.ValidationError.Description.Should().Be("Bike id is not valid.");

        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.Complete()).MustNotHaveHappened();
    }

    [Fact]
    public async void BikeRatingService_GetAverageRatingDetails_ReturnsOk()
    {
        // Arrange
        var bikeId = Guid.NewGuid();
        var listOfRating = A.Fake<List<BikeRating>>();

        A.CallTo(() => _unitOfWork.BikeRatingRepository.GetAll()).Returns(listOfRating);

        // Act
        var result = await _bikeRatingService.GetAverageRatingDetails(bikeId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult<BikeRatingDetailsDTO>>();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Message.Should().BeNull();
        result.ValidationError.Should().BeNull();

        result.Data.Should().NotBeNull();
        result.Data.NumberOfRating.Should().Be(0);
        result.Data.AverageRating.Should().Be(0);

        A.CallTo(() => _unitOfWork.BikeRatingRepository.GetAll()).MustHaveHappenedOnceExactly();
    }

}
