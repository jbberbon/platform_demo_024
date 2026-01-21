using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PlatformDemo.Core.Infrastructure;

public class PlatformDemoDbContextFactory : IDesignTimeDbContextFactory<PlatformDemoDbContext>
{
    public PlatformDemoDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PlatformDemoDbContext>();

        // SQLite connection string
        optionsBuilder.UseSqlite("Data Source=platform-demo.db");

        return new PlatformDemoDbContext(optionsBuilder.Options);
    }
}