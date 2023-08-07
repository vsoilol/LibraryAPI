using Microsoft.EntityFrameworkCore;
using WebLibrary.DataAccessLayer.DataAccess;
using WebLibrary.Domain.Entities;

namespace WebLibrary.DataAccessLayer.Repositories.UserRepositories;

internal class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Task<User?> GetUserByLoginAsync(string login)
    {
        return DbSet.AsNoTracking().FirstOrDefaultAsync(_ => _.Login == login);
    }
}