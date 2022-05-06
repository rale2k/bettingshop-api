using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Race : DomainEntityId
{
    [MaxLength(64)]
    public string Name { get; set; } = default!;
    public DateTime Time { get; set; } = default!;
    
    [MaxLength(128)]
    public string Location { get; set; } = default!;

    public ICollection<RaceHorse>? Horses { get; set; }
}