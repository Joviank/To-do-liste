using Scalar.AspNetCore;
using TaskService.Core.Repository;
using TaskServiceManager = TaskService.Core.TaskService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSingleton<ITaskRepository, FakeTaskRepository>();
builder.Services.AddSingleton<TaskServiceManager>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();
app.Run();