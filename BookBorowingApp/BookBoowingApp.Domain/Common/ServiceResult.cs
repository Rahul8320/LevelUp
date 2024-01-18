using System.Net;

namespace BookBoowingApp.Domain.Common;

public class ServiceResult(HttpStatusCode code, string? message = null)
{
    public HttpStatusCode StatusCode { get; set; } = code;
    public string? Message { get; set; } = message;
}

public class ServiceResult<T>(HttpStatusCode code, T data, string? message = null) : ServiceResult(code, message)
{
    public T Data { get; set; } = data;
}
