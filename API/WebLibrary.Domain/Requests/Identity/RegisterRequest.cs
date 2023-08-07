namespace WebLibrary.Domain.Requests.Identity;

public class RegisterRequest
{
    public string FirstName { get; set; } = null!;
    
    public string LastName { get; set; } = null!;
    
    public string MiddleName { get; set; } = null!;

    public string Login { get; set; } = null!;
    
    public string Password { get; set; } = null!;
}