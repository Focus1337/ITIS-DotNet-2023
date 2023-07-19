using System.Net.Mime;
using Back.Core.Interfaces;
using Back.Core.Models;
using Back.Web.Dto.Book;
using Back.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Back.Web.Controllers;

[ApiController]
[Route("[controller]"), OpenIddictAuthorize]
public class BooksController : CustomControllerBase
{
    private readonly IBookService _bookService;
    private readonly IUserService<User> _userService;
    private readonly ILogger<BooksController> _logger;

    public BooksController(IBookService bookService, IUserService<User> userService,
        ILogger<BooksController> logger)
    {
        _bookService = bookService;
        _userService = userService;
        _logger = logger;
    }

    [Produces(MediaTypeNames.Application.Json)]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation("Books GET executed");

        if (await _userService.GetCurrentUser() is null)
            return BadRequestDueToToken();

        var result = (from book in await _bookService.GetBooks()
            select new DisplayBookDto
                { Title = book.Title, Author = book.Author }).ToList();
        return Ok(result);
    }

    [Produces(MediaTypeNames.Application.Json)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        _logger.LogInformation("Books GET with param executed");

        if (await _userService.GetCurrentUser() is null)
            return BadRequestDueToToken();

        if (await _bookService.FindById(id) is not { } book)
            return NotFound();

        return Ok(new DisplayBookDto
            { Title = book.Title, Author = book.Author });
    }

    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] BookDto bookDto)
    {
        _logger.LogInformation("Books POST executed");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (await _userService.GetCurrentUser() is null)
            return BadRequestDueToToken();

        var book = new Book
        {
            Title = bookDto.Title,
            Author = bookDto.Author
        };

        var result = await _bookService.CreateBook(book);
        if (!result.IsSuccess)
            return BadRequest(result.Errors);
        return CreatedAtAction(nameof(Get), new
        {
            id = result.Value.Id
        }, result.Value.Id);
    }

    [Consumes(MediaTypeNames.Application.Json)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] BookDto bookDto)
    {
        _logger.LogInformation("Books PUT executed");

        if (!ModelState.IsValid)
            return BadRequest();

        if (await _userService.GetCurrentUser() is null)
            return BadRequestDueToToken();

        if (await _bookService.FindById(id) is not { } book)
            return NotFound();

        book.Title = bookDto.Title;
        var result = await _bookService.UpdateBook(book);
        if (!result.IsSuccess)
            return BadRequest(result.Errors.First().Message);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        _logger.LogInformation("Books DELETE executed");

        if (await _userService.GetCurrentUser() is null)
            return BadRequestDueToToken();

        if (await _bookService.FindById(id) is not { } book)
            return NotFound();

        await _bookService.DeleteBook(book);

        return NoContent();
    }
}