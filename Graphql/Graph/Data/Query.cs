using Graph.Models;

namespace Graph.Data;

public class Query
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Superhero> GetSuperheroes([Service] ApplicationDbContext context) =>
        context.Superheroes;
}