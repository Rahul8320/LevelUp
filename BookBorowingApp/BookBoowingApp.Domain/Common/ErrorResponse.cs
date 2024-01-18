using System.Net;

namespace BookBoowingApp.Domain.Common;

public class ErrorResponse(HttpStatusCode code, string? message = null)
{
    public HttpStatusCode ErrorCode { get; set; } = code;
    public string? Message { get; set; } = message;
}

public class ErrorResponse<T>(HttpStatusCode code, T errors, string? message = null) : ErrorResponse(code, message)
{
    public T Errors { get; set; } = errors;
}