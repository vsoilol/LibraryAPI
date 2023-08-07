using Mapster;
using WebLibrary.DataAccessLayer.Repositories.UserRepositories;
using WebLibrary.Domain.Dtos;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Requests.User;

namespace WebLibrary.BusinessLayer.Services.UserServices;

internal class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> GetUserByIdAsync(Guid id)
    {
        var userEntity = await _userRepository.GetByIdAsync(id);

        var mappedUser = userEntity?.Adapt<UserDto>();

        return mappedUser;
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var userEntities = await _userRepository.GetAsync();

        var mappedUsers = userEntities.Adapt<List<UserDto>>();

        return mappedUsers;
    }

    public async Task<bool> UpdateUserAsync(UpdateUserRequest request)
    {
        var userEntity = await _userRepository.GetByIdAsync(request.Id);

        if (userEntity is null)
        {
            return false;
        }

        request.Adapt(userEntity);
        
        await _userRepository.UpdateAsync(userEntity);
        return true;
    }

    public Task<bool> DeleteUserAsync(Guid id)
    {
        return _userRepository.DeleteAsync(new User { Id = id });
    }
}