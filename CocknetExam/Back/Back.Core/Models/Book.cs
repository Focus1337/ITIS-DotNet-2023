namespace Back.Core.Models;

public class Book
{
    public Guid Id { get; } = Guid.NewGuid();
    public required string Title { get; set; }
    public required string Author { get; set; }
}