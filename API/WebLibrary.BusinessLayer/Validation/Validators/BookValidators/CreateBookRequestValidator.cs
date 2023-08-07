using WebLibrary.BusinessLayer.Validation.Constants;
using WebLibrary.BusinessLayer.Validation.Models;
using WebLibrary.DataAccessLayer.Repositories.BookRepositories;
using WebLibrary.Domain.Requests.Book;

namespace WebLibrary.BusinessLayer.Validation.Validators.BookValidators;

internal class CreateBookRequestValidator : IValidator<CreateBookRequest>
{
    private readonly IBookRepository _bookRepository;

    public CreateBookRequestValidator(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<ValidationResult> IsInstanceValidAsync(CreateBookRequest instance)
    {
        var existingBook = await _bookRepository.GetByIsbnAsync(instance.Isbn);

        if (existingBook is null)
        {
            return ValidationResult.Success;
        }

        var errorMessage = string.Format(ValidationErrorMessages.BookWithIsbnAlreadyExists, instance.Isbn);
        var validationResult = new ValidationResult(new List<string> { errorMessage });
        return validationResult;
    }
}