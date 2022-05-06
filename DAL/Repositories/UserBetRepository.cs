using System.ComponentModel.Design;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class UserBetRepository: BaseEntityRepository<UserBet, AppDbContext>
{
    public UserBetRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public IEnumerable<UserBet> GetAllUserBets(int userId, bool noTracking = true)
    {
        return CreateQuery(noTracking).Where(e => e.UserId == userId).ToList();
    }

    public async Task<IEnumerable<UserBet>> GetAllUserBetsAsync(int userId, bool noTracking = true)
    {
        return await CreateQuery(noTracking).Where(e => e.UserId == userId).ToListAsync();
    }
    
    public UserBet? GetUserBetOnRace(int userId, int raceId, bool noTracking = true)
    {
        return CreateQuery(noTracking).Where(e => e.UserId == userId && e.RaceId == raceId).FirstOrDefault();
    }
    
    public async Task<UserBet?> GetUserBetOnRaceAsync(int userId, int raceId, bool noTracking = true)
    {
        return await CreateQuery(noTracking)
            .FirstOrDefaultAsync(e => e.UserId == userId && e.RaceId == raceId);
    }
    
    public UserBet? Remove(int userId, int raceId)
    {
        var entity = GetUserBetOnRace(userId, raceId);

        if (entity == null)
        {
            throw new NullReferenceException($"Entity {typeof(UserBet).Name} with userId {userId} " +
                                             $"and raceId {raceId} was not found");
        }

        return Remove(entity);
    }

}