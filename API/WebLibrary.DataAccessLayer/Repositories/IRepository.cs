using System.Linq.Expressions;
using WebLibrary.Domain.Entities;

namespace WebLibrary.DataAccessLayer.Repositories;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<List<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);

    Task<TEntity?> GetByIdAsync(Guid id);
    
    Task<TEntity> InsertAsync(TEntity entity);
    
    Task UpdateAsync(TEntity entity);

    Task<bool> DeleteAsync(TEntity entityToDelete);
}