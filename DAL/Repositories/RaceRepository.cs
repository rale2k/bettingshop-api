using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class RaceRepository: BaseEntityRepository<Race, AppDbContext>
{
    public RaceRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
    
    protected override IQueryable<Race> CreateQuery(bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        return query.Include(e => e.Horses);
    }
    
}