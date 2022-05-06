using Domain;

namespace DAL.Repositories;

public class RaceHorseRepository: BaseEntityRepository<RaceHorse, AppDbContext>
{
    public RaceHorseRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public IEnumerable<RaceHorse> GetAllHorsesInRace(int raceId, bool noTracking = true)
    {
        return CreateQuery(noTracking).Where(e => e.RaceId == raceId).ToList();
    }
}