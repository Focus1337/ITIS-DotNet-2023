using Graph.Data;
using Microsoft.EntityFrameworkCore;

namespace Graph.Models;

public class HeroMutation
{
    public async Task<Superhero> AddHero(CreateHeroDto dto, [Service] ApplicationDbContext dbContext)
    {
        // var ability = await dbContext.Abilities.FirstOrDefaultAsync(x => x.Id == Guid.Parse(dto.Abilities));
        // var abilities = new List<Ability> { ability };
        var hero = new Superhero
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Age = dto.Age,
            // Abilities = abilities
        };

        await dbContext.Superheroes.AddAsync(hero);
        await dbContext.SaveChangesAsync();
        return hero;
    }
}