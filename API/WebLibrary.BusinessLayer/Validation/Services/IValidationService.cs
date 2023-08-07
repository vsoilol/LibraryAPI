namespace WebLibrary.BusinessLayer.Validation.Services;

internal interface IValidationService
{
    Task ValidateAsync<T>(T instance);
}