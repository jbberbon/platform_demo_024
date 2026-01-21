namespace PlatformDemo.Core.Domain.Models;

public sealed class Timesheet
{
    public Guid Id { get; private set; }
    public string Description { get; private set; }
    public Guid ServicePlanId { get; private set; }
    public DateTimeOffset Start { get; private set; }
    public DateTimeOffset End { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }
    
    // Derived state
    public bool IsExpired => End <= CreatedAt;
    
    // Nav property
    public ServicePlan ServicePlan { get; private set; }

    // For EF Core only
    private Timesheet()
    {
    }

    public Timesheet(string description, Guid servicePlanId, DateTimeOffset start, DateTimeOffset end)
    {
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentNullException(nameof(description));
        if (end <= start)
            throw new ArgumentException("End must be after start.");

        Id = Guid.NewGuid();
        ServicePlanId = servicePlanId;
        Description = description;
        Start = start;
        End = end;
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}