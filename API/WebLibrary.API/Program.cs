using FluentMigrator.Runner;
using WebLibrary.API.Configurations;
using WebLibrary.API.Filters;
using WebLibrary.BusinessLayer;
using WebLibrary.DataAccessLayer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilterAttribute>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddDataAccessLayer(connectionString);
builder.Services.AddBusinessLayer();

builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

var isDevelopment = app.Environment.IsDevelopment();

if (isDevelopment)
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    var runner = services.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();