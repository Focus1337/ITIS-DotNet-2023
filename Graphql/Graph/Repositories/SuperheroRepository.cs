using Graph.Data;
using Graph.Interfaces;

namespace Graph.Repositories;

public class SuperheroRepository : ISuperheroRepository
{
    private readonly ApplicationDbContext _appDbContext;

    public SuperheroRepository(ApplicationDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
}
