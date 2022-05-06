using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class UserBetModel
{
    [Required]
    public int? UserId { get; set; }
    
    [Required]
    public int? RaceId { get; set; }
    
    [Required]
    public int? HorseId { get; set; }
}