namespace BookBorrowingApp.Application.Models;

public class Response
{
    public string Status { get; set; } = default!;
    public string? Message { get; set; }
}

public class Response<T> : Response
{
    public T Data { get; set; } = default!;
}

public class ErrorResponse<T> : Response
{
    public T Errors { get; set; } = default!;
}