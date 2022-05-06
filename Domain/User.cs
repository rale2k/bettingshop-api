using System.ComponentModel.DataAnnotations;

namespace Domain;

public class User : DomainEntityId
{
    [MaxLength(64)]
    public string Name { get; set; } = default!;
}
