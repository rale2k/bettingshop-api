using Api.Models;
using AutoMapper;
using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RaceController: ControllerBase
{
    private readonly AppUow _appUow;
    private readonly IMapper _mapper;
    
    public RaceController(AppUow appUow, IMapper mapper)
    {
        _appUow = appUow;
        _mapper = mapper;
    }

    private void GenerateRacePositions(int raceId)
    {
        var horses = _appUow.RaceHorses.GetAllHorsesInRace(raceId, false);

        Random random = new Random();

        // shuffle
        horses = horses.OrderBy(a => random.Next()).ToList();

        for (int i = 0; i < horses.Count(); i++)
        {
            horses.ElementAt(i).Position = i + 1;
        }

        _appUow.SaveChanges();
    }
    
    [HttpGet]
    public async Task<IEnumerable<RaceModel>> GetRaces()
    {
        var result = await _appUow.Races.GetAllAsync();
        
        return result.Select(e => _mapper.Map<RaceModel>(e)).ToList();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<RaceModel>> GetRace(int id)
    {
        var result = await _appUow.Races.FirstOrDefaultAsync(id);
        
        if (result == null)
        {
            return NotFound();
        }

        if (result.Time < DateTime.Now && result.Horses!.ElementAt(0).Position == null)
        {
            GenerateRacePositions(id);
            result = await _appUow.Races.FirstOrDefaultAsync(id);
        }
        
        return _mapper.Map<RaceModel>(result);
    }
    
    [HttpPost]
    public ActionResult PostRace(RaceModel race)
    {
        if (race.Horses.Count() == 0)
        {
            return BadRequest();
        }
        // prohibit position from being set when posting a race
        foreach (var raceHorse in race.Horses)
        {
            if (raceHorse.Position != null)
            {
                return BadRequest();
            }
        }

        Race newRace = _appUow.Races.Add(_mapper.Map<Race>(race));
        _appUow.SaveChanges();
        
        return CreatedAtAction("GetRace", new { id = newRace.Id }, _mapper.Map<RaceModel>(newRace));
    }
}