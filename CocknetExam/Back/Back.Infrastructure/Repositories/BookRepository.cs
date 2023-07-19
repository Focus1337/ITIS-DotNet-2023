using Back.Core.Interfaces;
using Back.Core.Models;
using Back.Infrastructure.Data;

namespace Back.Infrastructure.Repositories;

internal class BookRepository : EfRepository<Book, AppDbContext>, IBookRepository
{
    public BookRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}