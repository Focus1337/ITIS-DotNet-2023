using Back.Core.Interfaces;
using Back.Infrastructure.Data;
using Back.Infrastructure.Repositories;

namespace Back.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private IBookRepository? _bookRepository;
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext) =>
        _dbContext = dbContext;

    public IBookRepository Book => _bookRepository ??= new BookRepository(_dbContext);

    public async Task SaveChangesAsync() =>
        await _dbContext.SaveChangesAsync();
}