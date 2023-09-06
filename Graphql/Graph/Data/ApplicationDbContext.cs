using Graph.Data.ContextConfigurations;
using Graph.Models;
using Microsoft.EntityFrameworkCore;

namespace Graph.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Generate three GUIDS and place them in an arrays
        var ids = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

        // Apply configuration for the three contexts in our application
        // This will create the demo data for our GraphQL endpoint.
        builder.ApplyConfiguration(new SuperheroContextConfiguration(ids));
        builder.ApplyConfiguration(new AbilityContextConfiguration(ids));
    }

    // Add the DbSets for each of our models we would like at our database
    public DbSet<Superhero> Superheroes { get; set; } = null!;
    public DbSet<Ability> Abilities { get; set; } = null!;
}