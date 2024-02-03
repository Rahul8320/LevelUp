using System.Net;
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

        // Act
        var result = await _agreementService.CreateNewAgreement(addAgreementModel, userId);

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        result.Message.Should().BeNull();
        result.ValidationError.Should().BeNull();

        result.Data.Should().NotBeEmpty();
        result.Data.Should().NotBe(Guid.Empty);

    }
}
