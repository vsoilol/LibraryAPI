namespace WebLibrary.Domain.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; } = null!;
    
    public string LastName { get; set; } = null!;
    
    public string MiddleName { get; set; } = null!;

    public string Login { get; set; } = null!;
}