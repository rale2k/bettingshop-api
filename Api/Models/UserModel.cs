using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class UserModel
{
    public int Id { get; set; }
    
    [MaxLength(64)]
    public string Name { get; set; } = default!;
}