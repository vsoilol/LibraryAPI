using WebLibrary.BusinessLayer.Validation.Models;

namespace WebLibrary.BusinessLayer.Validation.Validators;

internal interface IValidator<in T>
{
    Task<ValidationResult> IsInstanceValidAsync(T instance);
}