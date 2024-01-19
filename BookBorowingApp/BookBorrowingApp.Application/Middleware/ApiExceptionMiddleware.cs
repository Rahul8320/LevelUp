using System.Net;
using BookBoowingApp.Domain.Common;
using Microsoft.AspNetCore.Diagnostics;

namespace BookBorrowingApp.Application.Middleware;

public class ApiExceptionMiddleware : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        ErrorResponse errorResponse;

        if (exception is ApiException)
        {
            var apiException = (exception as ApiException)!;
            httpContext.Response.StatusCode = (int)apiException.StatusCode;
            errorResponse = new ErrorResponse(code: apiException.StatusCode, message: apiException.Exception.Message);
            await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);
        }
        else
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            errorResponse = new ErrorResponse(code: HttpStatusCode.InternalServerError, message: exception.Message);
            await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);
        }

        return true;
    }
}
