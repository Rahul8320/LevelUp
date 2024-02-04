using System.Net;
using BookBoowingApp.Domain.Entities;
using BookBoowingApp.Infrastructure.IRepositories;
using BookBoowingApp.Service.Models;
using BookBoowingApp.Service.ServiceImplementations;
using FakeItEasy;
using FluentAssertions;

namespace BookBorrowingApp.UnitTests.ServiceTests;

public class AgreementServiceTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly AgreementService _agreementService;
    public AgreementServiceTests()
    {
        // Dependency.
        _unitOfWork = A.Fake<IUnitOfWork>();

        // SUT
        _agreementService = new AgreementService(_unitOfWork);
    }

    [Fact]
    public async void AgreementService_CreateNewAgreement_ReturnsGuid()
    {
        // Arrange
        var addAgreementModel = A.Fake<AddAgreementModel>();
        var userId = Guid.NewGuid();
        var bikeId = Guid.NewGuid();
        var bike = A.Fake<Bike>();
        bike.Id = bikeId;
        bike.IsAvailableForRent = true;
        addAgreementModel.BikeId = bikeId;

        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).Returns(bike);
        A.CallTo(() => _unitOfWork.Complete()).Returns(true);

        // Act
        var result = await _agreementService.CreateNewAgreement(addAgreementModel, userId);

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        result.Message.Should().BeNull();
        result.ValidationError.Should().BeNull();

        result.Data.Should().NotBeEmpty();
        result.Data.Should().NotBe(Guid.Empty);

        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.Complete()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async void AgreementService_CreateNewAgreement_ReturnsNotFound()
    {
        // Arrange
        var addAgreementModel = A.Fake<AddAgreementModel>();
        var userId = Guid.NewGuid();
        var bikeId = Guid.NewGuid();
        var bike = A.Fake<Bike>();
        bike.Id = bikeId;
        addAgreementModel.BikeId = bikeId;

        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId))!.Returns(Task.FromResult<Bike>(null!));

        // Act
        var result = await _agreementService.CreateNewAgreement(addAgreementModel, userId);

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        result.Message.Should().BeNull();
        result.ValidationError.Should().NotBeNull();
        result.ValidationError.Code.Should().Be("BikeNotFound");
        result.ValidationError.Description.Should().Be($"Bike with id: {bikeId} not found.");

        result.Data.Should().BeEmpty();

        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.Complete()).MustNotHaveHappened();
    }

    [Fact]
    public async void AgreementService_CreateNewAgreement_ReturnsBadRequest()
    {
        // Arrange
        var addAgreementModel = A.Fake<AddAgreementModel>();
        var userId = Guid.NewGuid();
        var bikeId = Guid.NewGuid();
        var bike = A.Fake<Bike>();
        bike.Id = bikeId;
        addAgreementModel.BikeId = bikeId;
        bike.IsAvailableForRent = false;

        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).Returns(bike);

        // Act
        var result = await _agreementService.CreateNewAgreement(addAgreementModel, userId);

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.Message.Should().BeNull();
        result.ValidationError.Should().NotBeNull();
        result.ValidationError.Code.Should().Be("NotAvailableForRent");
        result.ValidationError.Description.Should().Be($"Bike with id: {bikeId} is not available for rent.");

        result.Data.Should().BeEmpty();

        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.Complete()).MustNotHaveHappened();
    }
}
