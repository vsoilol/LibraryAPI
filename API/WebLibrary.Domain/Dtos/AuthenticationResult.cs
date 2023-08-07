namespace WebLibrary.Domain.Dtos;

public class AuthenticationResult
{
    public string? Token { get; set; }

    public bool Success { get; set; }

    public IEnumerable<string>? Errors { get; set; }

    public AuthenticationResult(IEnumerable<string> errors)
    {
        Errors = errors;
    }

    public AuthenticationResult(string token)
    {
        Success = true;
        Token = token;
    }
}