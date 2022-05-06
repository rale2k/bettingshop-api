using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class RaceModel
{
    public int? Id { get; set; }
    
    [MaxLength(64)]
    public string Name { get; set; } = default!;
    
    [Required]
    public DateTime? Time { get; set; }

    [MaxLength(128)]
    public string Location { get; set; } = default!;
    
    public ICollection<RaceHorseModel> Horses { get; set; } = default!;
}