using Back.Core.Models;
using FluentResults;

namespace Back.Core.Interfaces;

public interface IBookService
{
    Task<Book?> FindById(Guid id);
    Task<IEnumerable<Book>> GetBooks();
    Task<Result<Book>> CreateBook(Book book);
    Task<Result<Book>> UpdateBook(Book book);
    Task DeleteBook(Book book);
}