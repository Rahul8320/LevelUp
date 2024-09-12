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

    [Range(0.1, double.MaxValue)]
    public decimal? MaxPrice { get; set; }
    [Range(0.1, double.MaxValue)]
    public decimal? MinPrice { get; set; }

    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public SortBy SortBy { get; set; } = SortBy.Id;
    public SortOrder SortOrder { get; set; } = SortOrder.Asc;

    public string SearchTerm { get; set; } = string.Empty;
}
