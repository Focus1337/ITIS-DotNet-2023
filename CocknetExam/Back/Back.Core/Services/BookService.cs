using Back.Core.Interfaces;
using Back.Core.Models;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Back.Core.Services;

public class BookService : IBookService
{
    private readonly IUnitOfWork _unitOfWork;

    public BookService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Book?> FindById(Guid id) =>
        await _unitOfWork.Book.GetAll().FirstOrDefaultAsync(m => m.Id == id);

    public async Task<IEnumerable<Book>> GetBooks() =>
        await _unitOfWork.Book.GetAll().ToListAsync();

    public async Task<Result<Book>> CreateBook(Book book)
    {
        await _unitOfWork.Book.CreateAsync(book);
        await _unitOfWork.SaveChangesAsync();

        return await FindById(book.Id) is not { } res
            ? Result.Fail<Book>(new Error("Failed to create book"))
            : Result.Ok(res);
    }

    public async Task<Result<Book>> UpdateBook(Book book)
    {
        await _unitOfWork.Book.UpdateAsync(book);
        await _unitOfWork.SaveChangesAsync();

        return await FindById(book.Id) is not { } res
            ? Result.Fail<Book>(new Error("Failed to update book"))
            : Result.Ok(res);
    }

    public async Task DeleteBook(Book book)
    {
        await _unitOfWork.Book.DeleteAsync(book);
        await _unitOfWork.SaveChangesAsync();
    }
}