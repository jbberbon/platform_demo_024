namespace PlatformDemo.Web.Models.Common;

public class BasePaginatedResponse<T>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
    public List<T> Results { get; set; } = new();
}