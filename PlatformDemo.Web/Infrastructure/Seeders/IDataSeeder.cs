namespace PlatformDemo.Web.Infrastructure.Seeders;

/// <summary>
/// Standard interface for all seeders.
/// </summary>
public interface IDataSeeder
{
    Task SeedAsync();
}