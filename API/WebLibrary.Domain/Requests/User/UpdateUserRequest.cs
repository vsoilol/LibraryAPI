namespace WebLibrary.Domain.Requests.User;

public class UpdateUserRequest
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;
}