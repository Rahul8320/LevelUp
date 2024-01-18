using System.Net;

namespace BookBoowingApp.Domain.Common;

public class ServiceResult
{
    public HttpStatusCode StatusCode { get; set; } = default!;

    public ValidationError ValidationError { get; set; } = default!;
    public string? Message { get; set; } = default!;

    public ServiceResult(HttpStatusCode code)
    {
        StatusCode = code;
    }

    public ServiceResult(HttpStatusCode code, string? message = null)
    {
        StatusCode = code;
        Message = message;
    }

    public ServiceResult(HttpStatusCode code, ValidationError validationError)
    {
        StatusCode = code;
        ValidationError = validationError;
    }
}

public class ServiceResult<T> : ServiceResult
{
    public T Data { get; set; } = default!;

    public ServiceResult(HttpStatusCode code, T data) : base(code)
    {
        Data = data;
    }

    public ServiceResult(HttpStatusCode code, ValidationError validationError) : base(code, validationError)
    {
        Data = default!;
    }
}
