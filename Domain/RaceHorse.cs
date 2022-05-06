using System.ComponentModel.DataAnnotations;

namespace Domain;

public class RaceHorse : DomainEntityId
{
    public int RaceId { get; set; }
    
    public Race? Race { get; set; }
    
    [MaxLength(64)]
    public string Name { get; set; } = default!;
    
    public int? Position { get; set; }
}