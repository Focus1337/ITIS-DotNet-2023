using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Graph.Models;

public class Ability
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    [ForeignKey("SuperheroId")] 
    public Guid SuperheroId { get; set; }
}