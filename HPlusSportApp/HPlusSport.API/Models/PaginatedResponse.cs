using HPlusSport.API.Constants;

namespace HPlusSport.API.Models;

public class PaginatedResponse<T>
{
    public int PageSize { get; set; } = PaginatedResponseConstants.DefaultPageSize;
    public int PageNumber { get; set; } = PaginatedResponseConstants.DefaultPageNumber;
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<T> Items { get; set; } = [];
}
