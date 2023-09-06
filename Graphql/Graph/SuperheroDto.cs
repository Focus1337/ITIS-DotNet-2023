using Graph.Models;

namespace Graph;

public class CreateHeroDto
{
    public string Name { get; init; } = null!;
    public int Age { get; init; }
    public string Description { get; init; } = null!;
    // public string Abilities { get; init; }
}