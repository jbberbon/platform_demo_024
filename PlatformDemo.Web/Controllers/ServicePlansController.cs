using Microsoft.AspNetCore.Mvc;
using PlatformDemo.Web.Models;
using PlatformDemo.Web.Models.Common;
using PlatformDemo.Web.Queries;

namespace PlatformDemo.Web.Controllers;

public class ServicePlansController : Controller
{
    private readonly IServicePlanQueries _queries;
    private readonly ILogger<ServicePlansController> _logger;

    public ServicePlansController(IServicePlanQueries queries, ILogger<ServicePlansController> logger)
    {
        _queries = queries ?? throw new ArgumentNullException(nameof(queries));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    [HttpGet]
    public async Task<ActionResult<BasePaginatedResponse<ServicePlanViewModel>>> Index([FromQuery] BasePaginatedRequest request)
    {
        _logger.LogInformation($"Fetching service plans: Page {request.Page}, PageSize {request.PageSize}");
        var response = await _queries.GetListAsync(request);
        return View(response);
    }

    [HttpGet]
    public async Task<ActionResult<ServicePlanViewModel>> Details(Guid id)
    {
        _logger.LogInformation($"Fetching service plan: id: {id}");
        var response = await _queries.GetByIdAsync(id);
        
        if (response == null)
            return NotFound();

        return PartialView("_PlanDetails", response);
    }
    
    
}