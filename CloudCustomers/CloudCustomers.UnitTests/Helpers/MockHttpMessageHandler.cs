using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using Moq;
using Moq.Protected;

namespace CloudCustomers.UnitTests.Helpers;

internal static class MockHttpMessageHandler<T>
{
    internal static Mock<HttpMessageHandler> SetupBasicGetResourceList(List<T> expectedResponse)
    {
        var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonSerializer.Serialize(expectedResponse))
        };

        mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var mockHandler = new Mock<HttpMessageHandler>();

        mockHandler.Protected().Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()
        ).ReturnsAsync(mockResponse);

        return mockHandler;
    }
}
