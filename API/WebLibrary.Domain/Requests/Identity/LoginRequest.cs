namespace WebLibrary.Domain.Requests.Identity;

public class LoginRequest
{
    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;
}