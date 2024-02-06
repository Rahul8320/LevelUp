using System.Net;
using BookBoowingApp.Domain.Common;
using BookBoowingApp.Domain.Entities;
using BookBoowingApp.Domain.Enums;
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
    public async void BikeService_CreateNewBike_ReturnsGuid()
    {
        // Arrange
        var addBikeModel = A.Fake<AddBikeModel>();
        var userId = Guid.NewGuid();

        A.CallTo(() => _unitOfWork.Complete()).Returns(true);

        // Act
        var result = await _bikeService.CreateNewBike(addBikeModel, userId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult<Guid>>();
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        result.Message.Should().BeNull();
        result.ValidationError.Should().BeNull();
        result.Data.Should().NotBe(Guid.Empty);
        result.Data.Should().NotBeEmpty();

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
        result.Message.Should().BeNull();
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
        result.Message.Should().BeNull();
        result.ValidationError.Should().BeNull();

        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.BikeRepository.Delete(bike)).MustNotHaveHappened();
        A.CallTo(() => _unitOfWork.Complete()).MustNotHaveHappened();
    }

    [Fact]
    public async void BikeService_UpdateExistingBike_ReturnsBike()
    {
        // Arrange
        var bikeId = Guid.NewGuid();
        var addBikeModel = A.Fake<AddBikeModel>();
        var userId = Guid.NewGuid();
        var bike = A.Fake<Bike>();
        bike.Id = bikeId;
        bike.Owner = userId;
        var currentDateTime = DateTime.UtcNow;

        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).Returns(bike);
        A.CallTo(() => _unitOfWork.BikeRepository.Update(bike));
        A.CallTo(() => _unitOfWork.Complete()).Returns(true);

        // Act
        var result = await _bikeService.UpdateExistingBike(bikeId, addBikeModel, userId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult<Bike>>();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Message.Should().BeNull();
        result.ValidationError.Should().BeNull();
        result.Data.Should().NotBeNull();

        result.Data.Id.Should().Be(bikeId);
        result.Data.Owner.Should().Be(userId);
        result.Data.LastUpdated.Should().BeAfter(currentDateTime);


        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.BikeRepository.Update(bike)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.Complete()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async void BikeService_UpdateExistingBike_ReturnsForbidden()
    {
        // Arrange
        var bikeId = Guid.NewGuid();
        var addBikeModel = A.Fake<AddBikeModel>();
        var userId = Guid.NewGuid();
        var bike = A.Fake<Bike>();

        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).Returns(bike);
        A.CallTo(() => _unitOfWork.BikeRepository.Update(bike));
        A.CallTo(() => _unitOfWork.Complete()).Returns(true);

        // Act
        var result = await _bikeService.UpdateExistingBike(bikeId, addBikeModel, userId);

        // Assert
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult<Bike>>();
        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        result.Message.Should().BeNull();
        result.ValidationError.Should().BeNull();

        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.BikeRepository.Update(bike)).MustNotHaveHappened();
        A.CallTo(() => _unitOfWork.Complete()).MustNotHaveHappened();
    }

    [Fact]
    public async void BikeService_UpdateExistingBike_ReturnsNotFound()
    {
        // Arrange
        var bikeId = Guid.NewGuid();
        var addBikeModel = A.Fake<AddBikeModel>();
        var userId = Guid.NewGuid();
        var bike = A.Fake<Bike>();
        bike.Owner = userId;

        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId))!.Returns(Task.FromResult<Bike>(null!));
        A.CallTo(() => _unitOfWork.BikeRepository.Update(bike));
        A.CallTo(() => _unitOfWork.Complete()).Returns(true);

        // Act
        var result = await _bikeService.UpdateExistingBike(bikeId, addBikeModel, userId);

        // Assert
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult<Bike>>();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        result.Message.Should().BeNull();
        result.ValidationError.Should().BeNull();

        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.BikeRepository.Update(bike)).MustNotHaveHappened();
        A.CallTo(() => _unitOfWork.Complete()).MustNotHaveHappened();
    }

    [Fact]
    public async void BikeService_UpdateBikeAvailabilityStatus_ReturnsBike()
    {
        // Arrange
        var bike = A.Fake<Bike>();
        var isAvailableForRent = false;
        var currentBikeStatus = BikeStatus.Rented;
        var currentDateTime = DateTime.UtcNow;

        A.CallTo(() => _unitOfWork.BikeRepository.Update(bike));
        A.CallTo(() => _unitOfWork.Complete()).Returns(true);

        // Act
        var result = await _bikeService.UpdateBikeAvailabilityStatus(bike, isAvailableForRent, currentBikeStatus);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult<Bike>>();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Message.Should().BeNull();
        result.ValidationError.Should().BeNull();

        result.Data.Should().NotBeNull();
        result.Data.Id.Should().Be(bike.Id);
        result.Data.Owner.Should().Be(bike.Owner);
        result.Data.IsAvailableForRent.Should().Be(isAvailableForRent);
        result.Data.CurrentBikeStatus.Should().Be(currentBikeStatus);
        result.Data.LastUpdated.Should().BeAfter(currentDateTime);

        A.CallTo(() => _unitOfWork.BikeRepository.Update(bike)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.Complete()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async void BikeService_UpdateBikeRequestForReturnStatus_ReturnsBike()
    {
        // Arrange
        var bike = A.Fake<Bike>();
        var isRequestForReturn = true;
        var currentBikeStatus = BikeStatus.Rented;
        var currentDateTime = DateTime.UtcNow;

        A.CallTo(() => _unitOfWork.BikeRepository.Update(bike));
        A.CallTo(() => _unitOfWork.Complete()).Returns(true);

        // Act
        var result = await _bikeService.UpdateBikeRequestForReturnStatus(bike, isRequestForReturn, currentBikeStatus);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult<Bike>>();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Message.Should().BeNull();
        result.ValidationError.Should().BeNull();

        result.Data.Should().NotBeNull();
        result.Data.Id.Should().Be(bike.Id);
        result.Data.Owner.Should().Be(bike.Owner);
        result.Data.IsRequestForReturn.Should().Be(isRequestForReturn);
        result.Data.CurrentBikeStatus.Should().Be(currentBikeStatus);
        result.Data.LastUpdated.Should().BeAfter(currentDateTime);

        A.CallTo(() => _unitOfWork.BikeRepository.Update(bike)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.Complete()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async void BikeService_GetBikeDetails_ReturnsBike()
    {
        // Arrange
        var bikeId = Guid.NewGuid();
        var bike = A.Fake<Bike>();
        bike.Id = bikeId;

        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).Returns(bike);

        // Act
        var result = await _bikeService.GetBikeDetails(bikeId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult<Bike>>();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Message.Should().BeNull();
        result.ValidationError.Should().BeNull();

        result.Data.Should().NotBeNull();
        result.Data.Id.Should().Be(bikeId);

        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async void BikeService_GetBikeDetails_ReturnsNotFound()
    {
        // Arrange
        var bikeId = Guid.NewGuid();
        var bike = A.Fake<Bike>();
        bike.Id = bikeId;

        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId))!.Returns(Task.FromResult<Bike>(null!));

        // Act
        var result = await _bikeService.GetBikeDetails(bikeId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult<Bike>>();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        result.Message.Should().BeNull();
        result.ValidationError.Should().BeNull();

        result.Data.Should().BeNull();

        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async void BikeService_GetAllAvailableBike_ReturnsListOfBike()
    {
        // Arrange
        var bikeId = Guid.NewGuid();
        List<Bike> allBikes = [new Bike() {
            Id = bikeId,
            CoverImage = "Cover Image",
            CreatedAt = DateTime.UtcNow,
            CurrentBikeStatus = BikeStatus.AvailableForRent,
            Description = "Description",
            FuelCapacity = 15,
            FuelEconomy = 32,
            Images = ["images"],
            IsAvailableForRent = true,
            IsRequestForReturn = false,
            LastUpdated = DateTime.UtcNow,
            LateFeesPerDay = 1000,
            Maker = "Hero Motor Cops",
            Model = "Hardly Davidson X440",
            Owner = Guid.NewGuid(),
            RentalPricePerDay = 2000,
        }];

        A.CallTo(() => _unitOfWork.BikeRepository.GetAll()).Returns(allBikes);

        // Act
        var result = await _bikeService.GetAllAvailableBike();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult<List<Bike>>>();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Message.Should().BeNull();
        result.ValidationError.Should().BeNull();

        result.Data.Should().NotBeNull();
        result.Data.Should().HaveCount(1);
        result.Data[0].Id.Should().Be(bikeId);

        A.CallTo(() => _unitOfWork.BikeRepository.GetAll()).MustHaveHappenedOnceExactly();

    }

    [Fact]
    public async void BikeService_GetAllAvailableBike_ReturnsEmptyList()
    {
        // Arrange
        List<Bike> allBikes = [new Bike() {
            Id = Guid.NewGuid(),
            CoverImage = "Cover Image",
            CreatedAt = DateTime.UtcNow,
            CurrentBikeStatus = BikeStatus.AvailableForRent,
            Description = "Description",
            FuelCapacity = 15,
            FuelEconomy = 32,
            Images = ["images"],
            IsAvailableForRent = true,
            IsRequestForReturn = false,
            LastUpdated = DateTime.UtcNow,
            LateFeesPerDay = 1000,
            Maker = "Hero Motor Cops",
            Model = "Hardly Davidson X440",
            Owner = Guid.NewGuid(),
            RentalPricePerDay = 2000,
        }];

        A.CallTo(() => _unitOfWork.BikeRepository.GetAll()).Returns(allBikes);

        // Act
        var result = await _bikeService.GetAllAvailableBike("Royal Enfield", "Metor 350", 1000);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult<List<Bike>>>();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Message.Should().BeNull();
        result.ValidationError.Should().BeNull();

        result.Data.Should().NotBeNull();
        result.Data.Should().HaveCount(0);

        A.CallTo(() => _unitOfWork.BikeRepository.GetAll()).MustHaveHappenedOnceExactly();

    }

    [Fact]
    public async void BikeService_GetAllAvailableBike_ReturnsSingleBike()
    {
        // Arrange
        var bikeId = Guid.NewGuid();
        List<Bike> allBikes = [new Bike() {
            Id = Guid.NewGuid(),
            CoverImage = "Cover Image",
            CreatedAt = DateTime.UtcNow,
            CurrentBikeStatus = BikeStatus.AvailableForRent,
            Description = "Description",
            FuelCapacity = 15,
            FuelEconomy = 32,
            Images = ["images"],
            IsAvailableForRent = true,
            IsRequestForReturn = false,
            LastUpdated = DateTime.UtcNow,
            LateFeesPerDay = 1000,
            Maker = "Hero Motor Cops",
            Model = "Hardly Davidson X440",
            Owner = Guid.NewGuid(),
            RentalPricePerDay = 2000,
        },
        new Bike(){
            Id = bikeId,
            CoverImage = "Cover Image",
            CreatedAt = DateTime.UtcNow,
            CurrentBikeStatus = BikeStatus.AvailableForRent,
            Description = "Description",
            FuelCapacity = 15,
            FuelEconomy = 32,
            Images = ["images"],
            IsAvailableForRent = true,
            IsRequestForReturn = false,
            LastUpdated = DateTime.UtcNow,
            LateFeesPerDay = 1000,
            Maker = "Royal Enfield",
            Model = "Super Metor 650",
            Owner = Guid.NewGuid(),
            RentalPricePerDay = 1000,
        }];

        A.CallTo(() => _unitOfWork.BikeRepository.GetAll()).Returns(allBikes);

        // Act
        var result = await _bikeService.GetAllAvailableBike("Enfield", "Metor", 1000);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult<List<Bike>>>();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Message.Should().BeNull();
        result.ValidationError.Should().BeNull();

        result.Data.Should().NotBeNull();
        result.Data.Should().HaveCount(1);
        result.Data[0].Id.Should().Be(bikeId);
        result.Data[0].Maker.Should().Contain("Enfield");
        result.Data[0].Model.Should().Contain("Metor");
        result.Data[0].RentalPricePerDay.Should().Be(1000);

        A.CallTo(() => _unitOfWork.BikeRepository.GetAll()).MustHaveHappenedOnceExactly();

    }

}
