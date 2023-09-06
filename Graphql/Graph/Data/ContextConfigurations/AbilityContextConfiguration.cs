using Graph.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Graph.Data.ContextConfigurations;

public class AbilityContextConfiguration : IEntityTypeConfiguration<Ability>
{
    private readonly Guid[] _ids;

    public AbilityContextConfiguration(Guid[] ids)
    {
        _ids = ids;
    }

    public void Configure(EntityTypeBuilder<Ability> builder)
    {
        builder
            .HasData(
                new Ability
                {
                    Id = Guid.NewGuid(),
                    Name = "Intellect.",
                    SuperheroId = _ids[0]
                },
                new Ability
                {
                    Id = Guid.NewGuid(),
                    Name = "Fighting",
                    SuperheroId = _ids[0]
                },
                new Ability
                {
                    Id = Guid.NewGuid(),
                    Name = "Wealth.",
                    SuperheroId = _ids[0]
                },
                new Ability
                {
                    Id = Guid.NewGuid(),
                    Name = "Deflect blaster power.",
                    SuperheroId = _ids[1]
                },
                new Ability
                {
                    Id = Guid.NewGuid(),
                    Name = "Espionage",
                    SuperheroId = _ids[2]
                },
                new Ability
                {
                    Id = Guid.NewGuid(),
                    Name = "Infiltration",
                    SuperheroId = _ids[2]
                },
                new Ability
                {
                    Id = Guid.NewGuid(),
                    Name = "Subterfuge",
                    SuperheroId = _ids[2]
                });
    }
}