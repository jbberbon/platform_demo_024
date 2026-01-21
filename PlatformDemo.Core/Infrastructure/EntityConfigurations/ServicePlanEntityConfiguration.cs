using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlatformDemo.Core.Domain.Models;

namespace PlatformDemo.Core.Infrastructure.EntityConfigurations;

/// <summary>
/// EF Core configuration for the ServicePlan entity.
/// </summary>
public sealed class ServicePlanEntityConfiguration : IEntityTypeConfiguration<ServicePlan>
{
    public void Configure(EntityTypeBuilder<ServicePlan> builder)
    {
        // Primary key
        builder.HasKey(sp => sp.Id);

        // Scalar properties
        builder.Property(sp => sp.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(sp => sp.Description)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(sp => sp.CreatedAt)
            .IsRequired();

        builder.Property(sp => sp.UpdatedAt)
            .IsRequired();
        
        // Shadow property
        // SQLite cannot order by DateTimeOffset directly, so we use UtcTicks for database ordering
        builder.Property<long>("CreatedAtTicks")
            .HasComputedColumnSql("CAST(strftime('%s', CreatedAt) AS INTEGER)")
            .ValueGeneratedOnAddOrUpdate()
            .IsRequired();

        // Relationships
        builder.HasMany<Timesheet>(sp => sp.Timesheets) // One-to-many relationship between ServicePlan and Timesheet
            .WithOne(sp => sp.ServicePlan) 
            .HasForeignKey(sp => sp.ServicePlanId)
            .OnDelete(DeleteBehavior.Restrict); // Prevents ServicePlan deletion if Timesheets exist

        builder.Navigation(sp => sp.Timesheets)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}