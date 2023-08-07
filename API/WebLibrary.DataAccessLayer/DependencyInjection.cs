using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebLibrary.DataAccessLayer.DataAccess;
using WebLibrary.DataAccessLayer.Repositories.BookRepositories;
using WebLibrary.DataAccessLayer.Repositories.UserRepositories;
using WebLibrary.Migrations;

namespace WebLibrary.DataAccessLayer;

public static class DependencyInjection
{
    public static void AddDataAccessLayer(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        services
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddSqlServer()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(_202308060001_InitialTables).Assembly).For.Migrations());
        
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}