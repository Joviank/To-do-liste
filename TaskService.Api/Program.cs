using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using TaskService.Api.Data;
using TaskService.Api.Repository;
using TaskService.Core.Repository;
using TaskServiceManager = TaskService.Core.TaskService;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Default")
    ?? Environment.GetEnvironmentVariable("ConnectionString_Default");

builder.Services.AddOpenApi();
builder.Services.AddControllersWithViews();
builder.Services.AddHealthChecks();

builder.Services.AddDbContext<TaskDbContext>( options =>
        options.UseNpgsql(connectionString));

builder.Services.AddScoped<ITaskRepository, EfTaskRepository>();
builder.Services.AddScoped<TaskServiceManager>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.MapControllers();

// For frontend (Razor Pages)
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
// For frontend (Razor pages)

app.MapHealthChecks("/health");
app.Urls.Add("http://0.0.0.0:80");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TaskDbContext>();
    db.Database.Migrate();
}

app.Run();