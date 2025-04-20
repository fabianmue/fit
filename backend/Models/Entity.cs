using System.ComponentModel.DataAnnotations.Schema;

namespace FitBackend;

public class Entity
{
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public Guid Id { get; set; }
}

public record EntityReadDto
{
  public Guid Id { get; set; }
}
