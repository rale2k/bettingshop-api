using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class BaseEntityRepository<TEntity, TDbContext> : BaseEntityRepository<TEntity, int, TDbContext> 
    where TEntity : DomainEntityId
    where TDbContext: DbContext
{
    public BaseEntityRepository(TDbContext dbContext) : base(dbContext)
    {
    }
}

public class BaseEntityRepository<TEntity, TKey, TDbContext>
    where TEntity : DomainEntityId
    where TDbContext: DbContext
{
    protected readonly TDbContext RepoDbContext;
    protected readonly DbSet<TEntity> RepoDbSet;
    
    public BaseEntityRepository(TDbContext dbContext)
    {
        RepoDbContext = dbContext;
        RepoDbSet = dbContext.Set<TEntity>();
    }

    protected virtual IQueryable<TEntity> CreateQuery(bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }
    
    public virtual TEntity Add(TEntity entity)
    {
        return RepoDbSet.Add(entity).Entity;
    }

    public virtual TEntity Update(TEntity entity)
    {
        return RepoDbSet.Update(entity).Entity;
    }

    public virtual TEntity Remove(TEntity entity)
    {
        return RepoDbSet.Remove(entity).Entity;
    }

    public virtual TEntity Remove(TKey id)
    {
        var entity = FirstOrDefault(id);
        if (entity == null)
        {
            throw new NullReferenceException($"Entity {typeof(TEntity).Name} with id {id} was not found");
        }
        return Remove(entity);
    }

    public virtual TEntity? FirstOrDefault(TKey id, bool noTracking = true)
    {
       return CreateQuery(noTracking).FirstOrDefault(a => a.Id.Equals(id));
    }

    public virtual IEnumerable<TEntity> GetAll(bool noTracking = true)
    {
        return CreateQuery(noTracking).ToList();
    }

    public virtual bool Exists(TKey id)
    {
        return RepoDbSet.Any(a => a.Id.Equals(id));
    }

    public virtual async Task<TEntity?> FirstOrDefaultAsync(TKey id, bool noTracking = true)
    {
        return await CreateQuery(noTracking).FirstOrDefaultAsync(a => a.Id.Equals(id));
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(bool noTracking = true)
    {
        return await CreateQuery(noTracking).ToListAsync();
    }

    public virtual async Task<bool> ExistsAsync(TKey id)
    {
        return await RepoDbSet.AnyAsync(a => a.Id.Equals(id));
    }

    public virtual async Task<TEntity> RemoveAsync(TKey id)
    {
        var entity = await FirstOrDefaultAsync(id);
        if (entity == null)
        {
            throw new NullReferenceException($"Entity {typeof(TEntity).Name} with id {id} was not found");
        }
        return Remove(entity);
    }
}
