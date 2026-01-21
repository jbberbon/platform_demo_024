using Microsoft.EntityFrameworkCore;
using PlatformDemo.Core.Domain.Models;
using PlatformDemo.Core.Infrastructure;

namespace PlatformDemo.Web.Infrastructure.Seeders;

/// <summary>
/// Seeds initial ServicePlan data into the database.
/// 
/// <para>
/// Following DDD principles, we only seed data via the aggregate root (ServicePlan).
/// Timesheets are created and added through the ServicePlan aggregate, so no separate
/// Timesheet seeding is required.
/// </para>
/// </summary>
public class ServicePlanSeeder : IDataSeeder
{
    private readonly PlatformDemoDbContext _context;
    private readonly ILogger<ServicePlanSeeder> _logger;

    public ServicePlanSeeder(PlatformDemoDbContext context, ILogger<ServicePlanSeeder> logger)
    {
        _context = context ?? throw new ArgumentNullException(
            nameof(context),
            "Cannot create ServicePlanSeeder: PlatformDemoDbContext is missing. Ensure it is registered in DI before calling Seed()."
        );

        _logger = logger ?? throw new ArgumentNullException(
            nameof(logger),
            "Cannot create ServicePlanSeeder: ILogger<ServicePlanSeeder> is missing. Ensure logging is configured in the DI container."
        );
    }

    public async Task SeedAsync()
    {
        // Check if already seeded
        if (await _context.ServicePlans.AnyAsync())
        {
            _logger.LogInformation("Database already seeded. Skipping ServicePlanSeeder.");
            return;
        }
        
        _logger.LogInformation($"-----Seeding service plans...");
        var random = new Random();
        var servicePlans = new List<ServicePlan>();
        
        // Seed 10–15 ServicePlans
        int planCount = random.Next(10, 16);
        int totalTimesheets = 0;
        
        for (int i = 1; i <= planCount; i++)
        {
            var plan = new ServicePlan(
                name: $"Plan {i}",
                description: $"This is the description for Plan {i}."
            );

            // Add 0–5 Timesheets per plan
            int timesheetCount = random.Next(0, 6);
            totalTimesheets += timesheetCount;
            
            for (int t = 0; t < timesheetCount; t++)
            {
                var start = DateTimeOffset.UtcNow.AddDays(-random.Next(1, 30));
                var end = start.AddHours(random.Next(1, 8));

                var timesheet = new Timesheet(
                    description: $"Timesheet {t + 1} for Plan {i}",
                    servicePlanId: plan.Id,
                    start: start,
                    end: end
                );

                // Add via aggregate root
                plan.AddTimesheet(timesheet);
            }

            servicePlans.Add(plan);
            _logger.LogInformation($"----Seeded {planCount} service plans; added plan '{plan.Id}' with {timesheetCount} timesheets.");
        }
        
        // Save all plans (and nested timesheets) to the database
        _context.ServicePlans.AddRange(servicePlans);
        await _context.SaveChangesAsync();
        
        _logger.LogInformation($"-----Seeded {planCount} service plans and a total of {totalTimesheets} timesheets.");
    }
}