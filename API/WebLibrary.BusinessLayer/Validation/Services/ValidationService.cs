using WebLibrary.BusinessLayer.Exceptions;
using WebLibrary.BusinessLayer.Validation.Validators;

namespace WebLibrary.BusinessLayer.Validation.Services;

internal class ValidationService : IValidationService
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task ValidateAsync<T>(T instance)
    {
        var genericType = typeof(IValidator<>).MakeGenericType(typeof(T));

        if (_serviceProvider.GetService(genericType) is not IValidator<T> validator)
        {
            throw new NotSuchValidatorException(typeof(T));
        }
        
        var validationResult = await validator.IsInstanceValidAsync(instance);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors!.ToArray());
        }
    }
}