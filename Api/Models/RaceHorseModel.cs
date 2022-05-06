using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class RaceHorseModel
{
    public int? Id { get; set; }
    
    [MinLength(1), MaxLength(64)]
    public string Name { get; set; } = default!;
    public int? Position { get; set; }
}