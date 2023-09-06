using System.ComponentModel.DataAnnotations;

namespace Graph.Models;

public class Superhero
{
    [Key] public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Age { get; set; }

    public ICollection<Ability> Abilities { get; set; } = null!;
}