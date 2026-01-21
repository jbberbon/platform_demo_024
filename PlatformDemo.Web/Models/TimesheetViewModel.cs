namespace PlatformDemo.Web.Models;

public class TimesheetViewModel
{
    public string Id { get; set; }
    public string Description { get; set; }
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
    
    public bool IsExpired => End <= Start;
}