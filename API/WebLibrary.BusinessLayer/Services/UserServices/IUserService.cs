using WebLibrary.Domain.Dtos;
using WebLibrary.Domain.Requests.User;

namespace WebLibrary.BusinessLayer.Services.UserServices;

public interface IUserService
{
    Task<UserDto?> GetUserByIdAsync(Guid id);
    
    Task<List<UserDto>> GetAllUsersAsync();
    
    Task<bool> UpdateUserAsync(UpdateUserRequest request);
    
    Task<bool> DeleteUserAsync(Guid id);
}