namespace PlatformDemo.Web.Models.Common;

public class BasePaginatedRequest
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 5;
}