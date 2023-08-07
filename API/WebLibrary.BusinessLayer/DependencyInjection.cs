using System.Reflection;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using WebLibrary.BusinessLayer.Services.BookServices;
using WebLibrary.BusinessLayer.Services.IdentityServices;
using WebLibrary.BusinessLayer.Services.UserServices;
using WebLibrary.BusinessLayer.Validation.Services;
using WebLibrary.BusinessLayer.Validation.Validators;

namespace WebLibrary.BusinessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
    {
        RegisterMapster(services);
        RegisterValidators(services);
        services.AddScoped<IValidationService, ValidationService>();

        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IBookService, BookService>();

        return services;
    }

    private static void RegisterMapster(IServiceCollection services)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        var applicationAssembly = Assembly.GetExecutingAssembly();
        typeAdapterConfig.Scan(applicationAssembly);

        services.AddSingleton(typeAdapterConfig);
        services.AddScoped<IMapper, ServiceMapper>();
    }

    private static void RegisterValidators(IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        var validatorTypes = assembly
            .GetTypes()
            .Where(type => type.IsClass &&
                           type.GetInterfaces().Any(i =>
                               i.IsGenericType &&
                               i.GetGenericTypeDefinition() == typeof(IValidator<>)));

        foreach (var validatorType in validatorTypes)
        {
            var validatorInterface = validatorType.GetInterfaces()
                .FirstOrDefault(interfaceType => interfaceType.IsGenericType &&
                                                 interfaceType.GetGenericTypeDefinition() == typeof(IValidator<>));

            if (validatorInterface is not null)
            {
                services.AddScoped(validatorInterface, validatorType);
            }
        }
    }
}