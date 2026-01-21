using Microsoft.EntityFrameworkCore;
using PlatformDemo.Core.Infrastructure;
using PlatformDemo.Web.Infrastructure.Seeders;
using PlatformDemo.Web.Queries;

var builder = WebApplication.CreateBuilder(args);


// DbContext registration with SQLite
builder.Services.AddDbContext<PlatformDemoDbContext>(options =>
    options.UseSqlite("Data Source=platform-demo.db"));

// Register DI
builder.Services.AddTransient<IDataSeeder, ServicePlanSeeder>();
builder.Services.AddScoped<IServicePlanQueries, ServicePlanQueries>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Ensure database exists
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PlatformDemoDbContext>();
    await db.Database.MigrateAsync();

    // Seed data
    var seeders = scope.ServiceProvider.GetServices<IDataSeeder>();
    foreach (var seeder in seeders)
    {
        await seeder.SeedAsync();
    }
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ServicePlans}/{action=Index}/{id?}");

app.Run();