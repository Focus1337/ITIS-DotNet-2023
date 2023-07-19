using System.ComponentModel.DataAnnotations;

namespace Back.Web.Dto.Book;

public class DisplayBookDto
{
    [Required] public string Title { get; set; } = null!;

    [Required] public string Author { get; set; } = null!;
}