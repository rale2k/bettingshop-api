using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class UserRepository: BaseEntityRepository<User, AppDbContext>
{
    public UserRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
    
    public User? FirstOrDefault(string userName, bool noTracking = true)
    {
        return CreateQuery(noTracking).FirstOrDefault(a => a.Name.Equals(userName));
    }

    public async Task<User?> FirstOrDefaultAsync(string userName, bool noTracking = true)
    {
        return await CreateQuery(noTracking).FirstOrDefaultAsync(a => a.Name.Equals(userName));
    }
}