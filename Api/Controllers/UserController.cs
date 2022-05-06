using Api.Models;
using AutoMapper;
using DAL;
using DAL.Repositories;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController: ControllerBase
{
    private readonly AppUow _appUow;
    private readonly IMapper _mapper;
    
    public UserController(AppUow appUow, IMapper mapper)
    {
        _appUow = appUow;
        _mapper = mapper;
    }

    [HttpPost("Login")]
    public async Task<ActionResult<UserModel>> Login(UserModel user)
    {
        var result = await _appUow.Users.FirstOrDefaultAsync(user.Name);

        if (result != null)
        {
            return _mapper.Map<UserModel>(result);
        }

        User newUser;
        try
        {
            newUser = _appUow.Users.Add(_mapper.Map<User>(user));
            _appUow.SaveChanges();
        }
        catch (Exception)
        {
            return BadRequest();
        }
        
        return _mapper.Map<UserModel>(newUser);
    }
    
    [HttpGet("{userId}/bet/{raceId}")]
    public async Task<ActionResult<UserBetModel>> GetBet(int userId, int raceId)
    {
        var result = await _appUow.UserBets.GetUserBetOnRaceAsync(userId, raceId);

        if (result != null) 
            return _mapper.Map<UserBetModel>(result);

        return NotFound();
    }
    
    [HttpPost("{userId}/bet")]
    public async Task<ActionResult> PlaceBet(int userId, UserBetModel userBet)
    {
        // 1 bet per race for an user
        var race = await _appUow.UserBets
            .GetUserBetOnRaceAsync(userId, userBet.RaceId!.Value);
        if (race != null)
        {
            return BadRequest();            
        }
        
        UserBet? newUserBet;
        try
        {
            newUserBet = _appUow.UserBets.Add(_mapper.Map<UserBet>(userBet));
            _appUow.SaveChanges();
        }
        catch (Exception)
        {
            return NotFound();
        }
        
        return CreatedAtAction("GetBet", 
        new { userId = newUserBet.Id, raceId = newUserBet.RaceId},
            _mapper.Map<UserBetModel>(newUserBet));
    }

    [HttpDelete("{userId}/bet/{raceId}")]
    public IActionResult DeleteBet(int userId, int raceId)
    {
        try
        {
            _appUow.UserBets.Remove(userId, raceId);
        }
        catch (Exception e)
        {
            return NotFound(e);
        }
        
        _appUow.SaveChanges();

        return Ok();
    }

}