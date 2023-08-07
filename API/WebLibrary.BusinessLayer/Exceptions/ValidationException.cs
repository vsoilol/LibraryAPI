namespace WebLibrary.BusinessLayer.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(string[] errors) : base(BuildErrorMessage(errors))
    {
        Errors = errors;
    }

    private static string BuildErrorMessage(IEnumerable<string> errors)
    {
        var arr = errors
            .Select(x => $"{Environment.NewLine} -- {x}")
            .ToArray();
        return "Validation failed: " + string.Join(string.Empty, arr);
    }

    public string[] Errors { get; }
}