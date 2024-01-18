using System.Net;

namespace BookBoowingApp.Domain.Common;

public class ErrorResponse
{
    public HttpStatusCode ErrorCode { get; set; }
    public string? Message { get; set; }
}

public class ErrorResponse<T> : ErrorResponse
{
    public T Errors { get; set; } = default!;
}