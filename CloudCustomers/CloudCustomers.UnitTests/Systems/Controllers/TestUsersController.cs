using CloudCustomers.Api;
using CloudCustomers.Api.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CloudCustomers.UnitTests.Systems.Controllers;

public class TestUsersController
{
    [Fact]
    public async Task GetUsers_OnSuccess_ReturnsStatusCode200()
    {
        // Arrange
        var mockUserService = new Mock<IUserService>();

        var sut = new UsersController(mockUserService.Object);

        // Act
        var result = (OkObjectResult)await sut.GetUsers();

        // Assert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetUsers_OnSuccess_InvokeUserService()
    {
        // Arrange
        var mockUserService = new Mock<IUserService>();

        var sut = new UsersController(mockUserService.Object);

        // Act
        var result = (OkObjectResult)await sut.GetUsers();

        // Assert
        result.StatusCode.Should().Be(200);
    }
}
