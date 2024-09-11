using System.ComponentModel.DataAnnotations;
using HPlusSport.API.Constants;

namespace HPlusSport.API.Models;

public class RequestQueryParameters
{
    const int _maxPageSize = PaginatedResponseConstants.MaxPageSize;
    private int _pageSize = PaginatedResponseConstants.DefaultPageSize;

    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; } = PaginatedResponseConstants.DefaultPageNumber;

    [Range(1, _maxPageSize)]
    public int PageSize
    {
        get { return _pageSize; }
        set
        {
            _pageSize = Math.Min(_maxPageSize, value);
        }
    }

    public decimal? MaxPrice { get; set; }
    public decimal? MinPrice { get; set; }
}
