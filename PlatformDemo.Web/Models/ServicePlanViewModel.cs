namespace PlatformDemo.Web.Models;

public class ServicePlanViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTimeOffset DateOfPurchase { get; set; }
    public int TimesheetCount { get; set; }

    public List<TimesheetViewModel> Timesheets { get; set; } = new();
}