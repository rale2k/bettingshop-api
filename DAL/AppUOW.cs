using DAL.Repositories;

namespace DAL;

public class AppUow: BaseUow<AppDbContext>
{
    public AppUow(AppDbContext dbContext) : base(dbContext)
    {
    }
    
    private RaceHorseRepository? _raceHorses;
    private UserBetRepository? _userBets;
    private UserRepository? _users;
    private RaceRepository? _races;

    public virtual RaceHorseRepository RaceHorses =>
        _raceHorses ??= new RaceHorseRepository(DbContext);
    
    public virtual UserBetRepository UserBets =>
        _userBets ??= new UserBetRepository(DbContext);
    
    public virtual UserRepository Users =>
        _users ??= new UserRepository(DbContext);
    
    public virtual RaceRepository Races =>
        _races ??= new RaceRepository(DbContext);
    
}