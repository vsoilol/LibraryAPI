using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebLibrary.DataAccessLayer.DataAccess;
using WebLibrary.Domain.Entities;

namespace WebLibrary.DataAccessLayer.Repositories;

internal abstract class GenericRepository<TEntity> : IRepository<TEntity>
    where TEntity : Entity
{
    protected readonly ApplicationDbContext Context;
    protected readonly DbSet<TEntity> DbSet;

    protected GenericRepository(ApplicationDbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    public virtual Task<List<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
    {
        IQueryable<TEntity> query = DbSet;

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        if (orderBy is not null)
        {
            query = orderBy(query);
        }

        return query.AsNoTracking().ToListAsync();
    }

    public virtual Task<TEntity?> GetByIdAsync(Guid id)
    {
        var taskEntity = DbSet.AsNoTracking().FirstOrDefaultAsync(_ => _.Id == id);
        return taskEntity;
    }

    public virtual async Task<TEntity> InsertAsync(TEntity entity)
    {
        var createdBookEntity = DbSet.Add(entity).Entity;
        await Context.SaveChangesAsync();

        return createdBookEntity;
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        DbSet.Attach(entity);
        Context.Entry(entity).State = EntityState.Modified;

        await Context.SaveChangesAsync();
    }

    public virtual async Task<bool> DeleteAsync(TEntity entityToDelete)
    {
        if (Context.Entry(entityToDelete).State == EntityState.Detached)
        {
            DbSet.Attach(entityToDelete);
        }

        DbSet.Remove(entityToDelete);

        var affectedEntities = await Context.SaveChangesAsync();
        return affectedEntities > 0;
    }
}