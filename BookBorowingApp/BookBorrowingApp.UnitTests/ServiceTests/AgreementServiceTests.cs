using System.Net;
using BookBoowingApp.Domain.Common;
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
        result.Should().BeOfType<ServiceResult<Guid>>();
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
        result.Should().BeOfType<ServiceResult<Guid>>();
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
        result.Should().BeOfType<ServiceResult<Guid>>();
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.Message.Should().BeNull();
        result.ValidationError.Should().NotBeNull();
        result.ValidationError.Code.Should().Be("NotAvailableForRent");
        result.ValidationError.Description.Should().Be($"Bike with id: {bikeId} is not available for rent.");

        result.Data.Should().BeEmpty();

        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.Complete()).MustNotHaveHappened();
    }

    [Fact]
    public async void AgreementService_DeleteAgreement_ReturnsOk_ForNotAcceptedAgreement()
    {
        // Arrange
        var agreementId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var agreement = A.Fake<Agreement>();
        agreement.UserId = userId;
        agreement.IsAcceptedByUser = false;

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId)).Returns(agreement);
        A.CallTo(() => _unitOfWork.Complete()).Returns(true);

        // Act
        var result = await _agreementService.DeleteAgreement(agreementId, userId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult>();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Message.Should().BeNull();
        result.ValidationError.Should().BeNull();

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.Complete()).MustHaveHappenedOnceExactly();

    }

    [Fact]
    public async void AgreementService_DeleteAgreement_ReturnsOk_ForBikeOwner()
    {
        // Arrange
        var agreementId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var agreement = A.Fake<Agreement>();
        agreement.BikeOwnerId = userId;

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId)).Returns(agreement);
        A.CallTo(() => _unitOfWork.Complete()).Returns(true);

        // Act
        var result = await _agreementService.DeleteAgreement(agreementId, userId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult>();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Message.Should().BeNull();
        result.ValidationError.Should().BeNull();

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.Complete()).MustHaveHappenedOnceExactly();

    }

    [Fact]
    public async void AgreementService_DeleteAgreement_ReturnsNotFound()
    {
        // Arrange
        var agreementId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var agreement = A.Fake<Agreement>();

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId))!.Returns(Task.FromResult<Agreement>(null!));
        A.CallTo(() => _unitOfWork.Complete()).Returns(true);

        // Act
        var result = await _agreementService.DeleteAgreement(agreementId, userId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult>();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        result.Message.Should().Be($"Agreement with id: {agreementId} not found.");
        result.ValidationError.Should().BeNull();

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.Complete()).MustNotHaveHappened();

    }

    [Fact]
    public async void AgreementService_DeleteAgreement_ReturnsForbidden_ForAcceptedAgreement()
    {
        // Arrange
        var agreementId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var agreement = A.Fake<Agreement>();
        agreement.UserId = userId;
        agreement.IsAcceptedByUser = true;

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId)).Returns(agreement);
        A.CallTo(() => _unitOfWork.Complete()).Returns(true);

        // Act
        var result = await _agreementService.DeleteAgreement(agreementId, userId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult>();
        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        result.Message.Should().Be("You can't deleted this agreement as it's already accepted by you.");
        result.ValidationError.Should().BeNull();

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.Complete()).MustNotHaveHappened();

    }

    [Fact]
    public async void AgreementService_DeleteAgreement_ReturnsForbidden()
    {
        // Arrange
        var agreementId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var agreement = A.Fake<Agreement>();

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId)).Returns(agreement);
        A.CallTo(() => _unitOfWork.Complete()).Returns(true);

        // Act
        var result = await _agreementService.DeleteAgreement(agreementId, userId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult>();
        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        result.Message.Should().Be("You don't have permission for this operation.");
        result.ValidationError.Should().BeNull();

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.Complete()).MustNotHaveHappened();

    }

    [Fact]
    public async void AgreementService_UpdateExistingAgreement_ReturnsAgreement_ForNotAcceptedUser()
    {
        // Arrange
        var agreementId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var bikeId = Guid.NewGuid();
        var agreement = A.Fake<Agreement>();
        agreement.Id = agreementId;
        agreement.UserId = userId;
        agreement.BikeId = bikeId;
        agreement.IsAcceptedByUser = false;
        var addAgreementModel = A.Fake<AddAgreementModel>();
        addAgreementModel.BikeId = bikeId;
        var bike = A.Fake<Bike>();

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId)).Returns(agreement);
        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).Returns(bike);
        A.CallTo(() => _unitOfWork.Complete()).Returns(true);

        // Act
        var result = await _agreementService.UpdateExistingAgreement(agreementId, addAgreementModel, userId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult<Agreement>>();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Message.Should().BeNull();
        result.ValidationError.Should().BeNull();

        result.Data.Should().NotBeNull();
        result.Data.Id.Should().Be(agreementId);

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.Complete()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async void AgreementService_UpdateExistingAgreement_ReturnsAgreement_ForBikeOwner()
    {
        // Arrange
        var agreementId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var bikeId = Guid.NewGuid();
        var agreement = A.Fake<Agreement>();
        agreement.Id = agreementId;
        agreement.BikeOwnerId = userId;
        agreement.BikeId = bikeId;
        agreement.IsAcceptedByUser = false;
        var addAgreementModel = A.Fake<AddAgreementModel>();
        addAgreementModel.BikeId = bikeId;
        var bike = A.Fake<Bike>();

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId)).Returns(agreement);
        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).Returns(bike);
        A.CallTo(() => _unitOfWork.Complete()).Returns(true);

        // Act
        var result = await _agreementService.UpdateExistingAgreement(agreementId, addAgreementModel, userId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult<Agreement>>();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Message.Should().BeNull();
        result.ValidationError.Should().BeNull();

        result.Data.Should().NotBeNull();
        result.Data.Id.Should().Be(agreementId);

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.Complete()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async void AgreementService_UpdateExistingAgreement_ReturnsNotFound_ForAgreement()
    {
        // Arrange
        var agreementId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var bikeId = Guid.NewGuid();
        var agreement = A.Fake<Agreement>();
        agreement.Id = agreementId;
        agreement.UserId = userId;
        var addAgreementModel = A.Fake<AddAgreementModel>();
        addAgreementModel.BikeId = bikeId;
        var bike = A.Fake<Bike>();

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId))!.Returns(Task.FromResult<Agreement>(null!));
        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).Returns(bike);
        A.CallTo(() => _unitOfWork.Complete()).Returns(true);

        // Act
        var result = await _agreementService.UpdateExistingAgreement(agreementId, addAgreementModel, userId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult<Agreement>>();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        result.Message.Should().BeNull();
        result.ValidationError.Should().NotBeNull();
        result.ValidationError.Code.Should().Be("NotFoundAgreement");
        result.ValidationError.Description.Should().Be($"Agreement with id: {agreementId} is not found!");

        result.Data.Should().BeNull();

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).MustNotHaveHappened();
        A.CallTo(() => _unitOfWork.Complete()).MustNotHaveHappened();
    }

    [Fact]
    public async void AgreementService_UpdateExistingAgreement_ReturnsNotFound_ForBike()
    {
        // Arrange
        var agreementId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var bikeId = Guid.NewGuid();
        var agreement = A.Fake<Agreement>();
        agreement.Id = agreementId;
        agreement.UserId = userId;
        agreement.BikeId = bikeId;
        var addAgreementModel = A.Fake<AddAgreementModel>();
        addAgreementModel.BikeId = bikeId;
        var bike = A.Fake<Bike>();

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId)).Returns(agreement);
        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId))!.Returns(Task.FromResult<Bike>(null!));
        A.CallTo(() => _unitOfWork.Complete()).Returns(true);

        // Act
        var result = await _agreementService.UpdateExistingAgreement(agreementId, addAgreementModel, userId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult<Agreement>>();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        result.Message.Should().BeNull();
        result.ValidationError.Should().NotBeNull();
        result.ValidationError.Code.Should().Be("NotFoundBike");
        result.ValidationError.Description.Should().Be($"Bike with id: {bikeId} is not found!");

        result.Data.Should().BeNull();

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.Complete()).MustNotHaveHappened();
    }

    [Fact]
    public async void AgreementService_UpdateExistingAgreement_ReturnsForbidden_ForAcceptedAgreement()
    {
        // Arrange
        var agreementId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var bikeId = Guid.NewGuid();
        var agreement = A.Fake<Agreement>();
        agreement.Id = agreementId;
        agreement.UserId = userId;
        agreement.BikeId = bikeId;
        agreement.IsAcceptedByUser = true;
        var addAgreementModel = A.Fake<AddAgreementModel>();
        addAgreementModel.BikeId = bikeId;
        var bike = A.Fake<Bike>();

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId)).Returns(agreement);
        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId))!.Returns(Task.FromResult<Bike>(null!));
        A.CallTo(() => _unitOfWork.Complete()).Returns(true);

        // Act
        var result = await _agreementService.UpdateExistingAgreement(agreementId, addAgreementModel, userId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult<Agreement>>();
        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        result.Message.Should().BeNull();
        result.ValidationError.Should().NotBeNull();
        result.ValidationError.Code.Should().Be("PermissionDenied");
        result.ValidationError.Description.Should().Be("You can't deleted this agreement as it's already accepted by you.");

        result.Data.Should().BeNull();

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).MustNotHaveHappened();
        A.CallTo(() => _unitOfWork.Complete()).MustNotHaveHappened();
    }

    [Fact]
    public async void AgreementService_UpdateExistingAgreement_ReturnsForbidden()
    {
        // Arrange
        var agreementId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var bikeId = Guid.NewGuid();
        var agreement = A.Fake<Agreement>();
        agreement.Id = agreementId;
        agreement.BikeId = bikeId;
        agreement.IsAcceptedByUser = true;
        var addAgreementModel = A.Fake<AddAgreementModel>();
        addAgreementModel.BikeId = bikeId;
        var bike = A.Fake<Bike>();

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId)).Returns(agreement);
        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId))!.Returns(Task.FromResult<Bike>(null!));
        A.CallTo(() => _unitOfWork.Complete()).Returns(true);

        // Act
        var result = await _agreementService.UpdateExistingAgreement(agreementId, addAgreementModel, userId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult<Agreement>>();
        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        result.Message.Should().BeNull();
        result.ValidationError.Should().NotBeNull();
        result.ValidationError.Code.Should().Be("PermissionDenied");
        result.ValidationError.Description.Should().Be("You don't have permission for this operation.");

        result.Data.Should().BeNull();

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.BikeRepository.Get(bikeId)).MustNotHaveHappened();
        A.CallTo(() => _unitOfWork.Complete()).MustNotHaveHappened();
    }

    [Fact]
    public async void AgreementService_GetAgreementDetails_ReturnsAgreement_ForAgreementUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var agreementId = Guid.NewGuid();
        var agreement = A.Fake<Agreement>();
        agreement.Id = agreementId;
        agreement.UserId = userId;

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId)).Returns(agreement);

        // Act
        var result = await _agreementService.GetAgreementDetails(agreementId, userId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult<Agreement>>();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Message.Should().BeNull();
        result.ValidationError.Should().BeNull();

        result.Data.Should().NotBeNull();
        result.Data.Id.Should().Be(agreementId);

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async void AgreementService_GetAgreementDetails_ReturnsAgreement_ForBikeOwner()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var agreementId = Guid.NewGuid();
        var agreement = A.Fake<Agreement>();
        agreement.Id = agreementId;
        agreement.BikeOwnerId = userId;

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId)).Returns(agreement);

        // Act
        var result = await _agreementService.GetAgreementDetails(agreementId, userId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult<Agreement>>();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Message.Should().BeNull();
        result.ValidationError.Should().BeNull();

        result.Data.Should().NotBeNull();
        result.Data.Id.Should().Be(agreementId);

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async void AgreementService_GetAgreementDetails_ReturnsNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var agreementId = Guid.NewGuid();

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId))!.Returns(Task.FromResult<Agreement>(null!));

        // Act
        var result = await _agreementService.GetAgreementDetails(agreementId, userId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult<Agreement>>();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        result.Message.Should().BeNull();
        result.ValidationError.Should().NotBeNull();
        result.ValidationError.Code.Should().Be("NotFoundAgreement");
        result.ValidationError.Description.Should().Be($"Agreement with id: {agreementId} not found!");

        result.Data.Should().BeNull();

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async void AgreementService_GetAgreementDetails_ReturnsForbidden()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var agreementId = Guid.NewGuid();
        var agreement = A.Fake<Agreement>();
        agreement.Id = agreementId;

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId)).Returns(agreement);

        // Act
        var result = await _agreementService.GetAgreementDetails(agreementId, userId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResult<Agreement>>();
        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        result.Message.Should().BeNull();
        result.ValidationError.Should().NotBeNull();
        result.ValidationError.Code.Should().Be("PermissionDenied");
        result.ValidationError.Description.Should().Be("You don't have the permission for this operation!");

        result.Data.Should().BeNull();

        A.CallTo(() => _unitOfWork.AgreementRepository.Get(agreementId)).MustHaveHappenedOnceExactly();
    }


}
