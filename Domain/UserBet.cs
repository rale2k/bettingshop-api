using System.ComponentModel.DataAnnotations;

namespace Domain;

public class UserBet : DomainEntityId
{
    public int RaceId { get; set; }
    public Race? Race { get; set; }
    
    public int UserId { get; set; }
    public User? User { get; set; }
    
    public int HorseId { get; set; }
    public RaceHorse? Horse { get; set; }
}
