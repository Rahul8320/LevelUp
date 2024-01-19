using System.Net;

namespace BookBoowingApp.Domain.Common;

public class ApiException(HttpStatusCode code, Exception exception) : Exception
{
    public HttpStatusCode StatusCode { get; set; } = code;
    public Exception Exception { get; set; } = exception;
}
