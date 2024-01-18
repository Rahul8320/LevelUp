using System.Net;

namespace BookBoowingApp.Domain.Common;

public class ApiException(HttpStatusCode code, string? message = null) : Exception
{
    public HttpStatusCode StatusCode { get; set; } = code;
    public string? ExMessage { get; set; } = message;
}

public class ApiException<T>(HttpStatusCode code, T errors, string? message = null) : ApiException(code, message)
{
    public T Errors { get; set; } = errors;
}
