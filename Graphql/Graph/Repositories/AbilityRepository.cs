using Graph.Data;
using Graph.Interfaces;

namespace Graph.Repositories;

public class AbilityRepository : ISuperpowerRepository
{
    private readonly ApplicationDbContext _appDbContext;

    public AbilityRepository(ApplicationDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
}