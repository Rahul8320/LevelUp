using CloudCustomers.Api;
using CloudCustomers.Api.Controllers;
using CloudCustomers.Api.Models;
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
        mockUserService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync([
                new(){
                    Id = 1,
                    Name = "Test User",
                    Address = new(){
                        Street = "Test Street",
                        City = "Test City",
                        ZipCode = 000000,
                    },
                    Email = "test@testingteam.com"
                }
            ]);

        var sut = new UsersController(mockUserService.Object);

        // Act
        var result = (OkObjectResult)await sut.GetUsers();

        // Assert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetUsers_OnSuccess_InvokeUserServiceExactlyOnce()
    {
        // Arrange
        var mockUserService = new Mock<IUserService>();
        mockUserService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());

        var sut = new UsersController(mockUserService.Object);

        // Act
        var result = await sut.GetUsers();

        // Assert
        mockUserService.Verify(service => service.GetAllUsers(), Times.Once);

    }

    [Fact]
    public async Task GetUsers_OnSuccess_ReturnsListOfUsers()
    {
        // Arrange
        var mockUserService = new Mock<IUserService>();
        mockUserService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync([
                new(){
                    Id = 1,
                    Name = "Test User",
                    Address = new(){
                        Street = "Test Street",
                        City = "Test City",
                        ZipCode = 000000,
                    },
                    Email = "test@testingteam.com"
                }
            ]);

        var sut = new UsersController(mockUserService.Object);

        // Act
        var result = await sut.GetUsers();

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var data = (OkObjectResult)result;
        data.Value.Should().BeOfType<List<User>>();

    }

    [Fact]
    public async Task GetUsers_OnUsersNotFound_Returns400()
    {
        // Arrange
        var mockUserService = new Mock<IUserService>();
        mockUserService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());

        var sut = new UsersController(mockUserService.Object);

        // Act
        var result = await sut.GetUsers();

        // Assert
        result.Should().BeOfType<NotFoundResult>();

    }
}
