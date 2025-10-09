namespace WebApi.Models.Pagination;

public class PagedRequest
{
    private int _pageNumber = 1;
    private int _pageSize = 20;
    
    public int PageNumber 
    { 
        get => _pageNumber; 
        set => _pageNumber = value < 1 ? 1 : value; 
    }
    
    public int PageSize 
    { 
        get => _pageSize; 
        set => _pageSize = value switch
        {
            < 1 => 1,
            > PaginationConstants.MaxPageSize => PaginationConstants.MaxPageSize,
            _ => value
        };
    }
    
    public string? Search { get; set; }
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; } = false;
}

public class PagedRequest<TFilter> : PagedRequest where TFilter : class
{
    public TFilter? Filter { get; set; }
}