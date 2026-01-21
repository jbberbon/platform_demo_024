using Microsoft.EntityFrameworkCore;
using PlatformDemo.Core.Domain.Models;
using PlatformDemo.Core.Infrastructure;
using PlatformDemo.Web.Models;
using PlatformDemo.Web.Models.Common;

namespace PlatformDemo.Web.Queries;

public class ServicePlanQueries : IServicePlanQueries
{
    private readonly PlatformDemoDbContext _context;
    private readonly ILogger<ServicePlanQueries> _logger;

    public ServicePlanQueries(PlatformDemoDbContext context, ILogger<ServicePlanQueries> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<BasePaginatedResponse<ServicePlanViewModel>> GetListAsync(BasePaginatedRequest request)
    {
        if (request.Page <= 0) request.Page = 1;
        if (request.PageSize <= 0) request.PageSize = 10;

        _logger.LogInformation(
            "Fetching ServicePlans page {Page} with page size {PageSize}",
            request.Page,
            request.PageSize
        );

        var query = _context.ServicePlans
            .AsNoTracking();

        var total = await query.CountAsync();

        var results = await query
            .OrderByDescending(sp => EF.Property<long>(sp, "CreatedAtTicks"))
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(sp => new ServicePlanViewModel
            {
                Id = sp.Id.ToString(),
                Name = sp.Name,
                Description = sp.Description,
                DateOfPurchase = sp.DateOfPurchase,
                TimesheetCount = sp.Timesheets.Count
            })
            .ToListAsync();

        return new BasePaginatedResponse<ServicePlanViewModel>
        {
            Page = request.Page,
            PageSize = request.PageSize,
            Total = total,
            Results = results
        };
    }

    public async Task<ServicePlanViewModel?> GetByIdAsync(Guid id)
    {
        var plan = await _context.ServicePlans
            .AsNoTracking()
            .Include(p => p.Timesheets)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (plan is null)
            return null;

        return MapToViewModel(plan);
    }

    private ServicePlanViewModel MapToViewModel(ServicePlan servicePlan)
    {
        return new ServicePlanViewModel()
        {
            Id = servicePlan.Id.ToString(),
            Name = servicePlan.Name,
            Description = servicePlan.Description,
            DateOfPurchase = servicePlan.DateOfPurchase,
            TimesheetCount = servicePlan.Timesheets.Count,
            Timesheets = servicePlan.Timesheets.Select(ts => new TimesheetViewModel
            {
                Id = ts.Id.ToString(),
                Description = ts.Description,
                Start = ts.Start,
                End = ts.End
            }).ToList()
        };
    }
}