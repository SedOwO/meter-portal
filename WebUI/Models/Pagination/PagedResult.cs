namespace WebUI.Models.Pagination
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public PaginationInfo Pagination { get; set; } = new();
    }
}
