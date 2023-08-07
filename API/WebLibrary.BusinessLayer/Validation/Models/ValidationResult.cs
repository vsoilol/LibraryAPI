namespace WebLibrary.BusinessLayer.Validation.Models;

public class ValidationResult
{
    public bool IsValid { get; set; }

    public List<string>? Errors { get; set; }

    public ValidationResult()
    {
        IsValid = true;
    }

    public ValidationResult(List<string> errors)
    {
        IsValid = false;
        Errors = errors;
    }

    public static ValidationResult Success => new();
}