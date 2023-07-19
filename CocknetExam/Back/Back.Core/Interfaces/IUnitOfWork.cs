namespace Back.Core.Interfaces;

public interface IUnitOfWork
{
    IBookRepository Book { get; }
    Task SaveChangesAsync();
}