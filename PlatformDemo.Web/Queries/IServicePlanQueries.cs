using PlatformDemo.Web.Models;
using PlatformDemo.Web.Models.Common;

namespace PlatformDemo.Web.Queries;

public interface IServicePlanQueries
{
    Task<BasePaginatedResponse<ServicePlanViewModel>> GetListAsync(BasePaginatedRequest request);
    Task<ServicePlanViewModel?> GetByIdAsync(Guid id);
}