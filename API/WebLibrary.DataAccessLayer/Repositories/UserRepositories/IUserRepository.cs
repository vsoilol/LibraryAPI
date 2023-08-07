using WebLibrary.Domain.Entities;

namespace WebLibrary.DataAccessLayer.Repositories.UserRepositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByLoginAsync(string login);
}