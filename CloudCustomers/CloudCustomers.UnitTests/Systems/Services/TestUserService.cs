using CloudCustomers.Api;
using CloudCustomers.Api.Models;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;

namespace CloudCustomers.UnitTests.Systems.Services;

public class TestUserService
{
    [Fact]
    public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
    {
        // Arrange
        var expectedResponse = UsersFixtures.GetTestUsers();
        var mockHandler = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(mockHandler.Object);
        var sut = new UserService(httpClient);

        // Act
        await sut.GetAllUsers();

        // Assert
        // Verify HTTP Request is made!
        mockHandler.Protected().Verify(
            "SendAsync",
            Times.Exactly(1),
            ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
            ItExpr.IsAny<CancellationToken>()
        );

    }

    [Fact]
    public async Task GetAllUsers_OnSuccess_ReturnsListOfUsers()
    {
        // Arrange
        var expectedResponse = UsersFixtures.GetTestUsers();
        var mockHandler = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(mockHandler.Object);
        var sut = new UserService(httpClient);

        // Act
        var result = await sut.GetAllUsers();

        // Assert
        result.Should().BeOfType<List<User>>();

    }

    [Fact]
    public async Task GetAllUsers_OnNotFound_Returns404()
    {
        // Arrange
        var expectedResponse = new List<User> { };
        var mockHandler = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(mockHandler.Object);
        var sut = new UserService(httpClient);

        // Act
        var result = await sut.GetAllUsers();

        // Assert
        result.Should().BeOfType<NotFoundResult>();

    }
}
