using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlatformDemo.Core.Domain.Models;

namespace PlatformDemo.Core.Infrastructure.EntityConfigurations;

public sealed class TimesheetEntityConfiguration : IEntityTypeConfiguration<Timesheet>
{
    public void Configure(EntityTypeBuilder<Timesheet> builder)
    {
        // Primary key
        builder.HasKey(ts => ts.Id);

        // Scalar properties
        builder.Property(ts => ts.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(ts => ts.Start)
            .IsRequired();

        builder.Property(ts => ts.End)
            .IsRequired();

        builder.Property(ts => ts.CreatedAt)
            .IsRequired();

        builder.Property(ts => ts.UpdatedAt)
            .IsRequired();

        // Relationship is already handled in ServicePlan aggregate root
    }
}