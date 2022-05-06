using Microsoft.EntityFrameworkCore;

namespace DAL;

public class BaseUow<TDbContext>
    where TDbContext : DbContext
{
    protected readonly TDbContext DbContext;

    public BaseUow(TDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public virtual async Task<int> SaveChangesAsync()
    {
        return await DbContext.SaveChangesAsync();
    }

    public virtual int SaveChanges()
    {
            return DbContext.SaveChanges();
    }
}
