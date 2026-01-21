namespace PlatformDemo.Core.Domain.Models;

public sealed class ServicePlan
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset DateOfPurchase => CreatedAt;
    public DateTimeOffset UpdatedAt { get; private set; }
    
    // Nav property
    private readonly List<Timesheet> _timesheets = new();
    public IReadOnlyCollection<Timesheet> Timesheets => _timesheets;
    
    // Computed properties

    // For EF Core only
    private ServicePlan()
    {
    }

    public ServicePlan(string name, string description, List<Timesheet>? timesheets = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;
        
        if (timesheets != null)
        {
            _timesheets.AddRange(timesheets);
        }
    }

    public void AddTimesheet(Timesheet timesheet) => _timesheets.Add(timesheet);
}