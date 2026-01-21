using Microsoft.EntityFrameworkCore;
using PlatformDemo.Core.Domain.Models;

namespace PlatformDemo.Core.Infrastructure;

/// <summary>
/// Entity Framework Core database context for the PlatformDemo application.
/// Uses SQLite as the underlying database.
/// </summary>
public class PlatformDemoDbContext : DbContext
{
    public PlatformDemoDbContext(DbContextOptions<PlatformDemoDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<ServicePlan> ServicePlans => Set<ServicePlan>();
    public DbSet<Timesheet> Timesheets => Set<Timesheet>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all IEntityTypeConfiguration<T> in this assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlatformDemoDbContext).Assembly);
    }
}

