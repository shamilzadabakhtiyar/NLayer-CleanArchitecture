using App.Application.Extensions;
using App.Persistence.Extensions;
using CleanApp.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllersWithFiltersExt()
    .AddSwaggerExt()
    .AddExceptionHandlerExt()
    .AddCachingExt();

builder.Services
    .AddRepositories(builder.Configuration)
    .AddServices(builder.Configuration);

var app = builder.Build();

app.UseConfigurePipelineExt();

app.MapControllers();

app.Run();
