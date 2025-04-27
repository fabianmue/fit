using System.ComponentModel.DataAnnotations;

namespace FitBackend;

public class Link : Entity
{
  [Required]
  public required string Label { get; set; }

  [Required]
  public required string Url { get; set; }

  // relationships
  public Guid CompanyId { get; set; }

  public Company Company { get; set; } = null!;
}

#pragma warning disable CS8618 // Dto classes
public record LinkReadDto
{
  [Required]
  public string Label { get; set; }

  [Required]
  public string Url { get; set; }
}

public record LinkCreateDto
{
  [Required]
  public string Label { get; set; }

  [Required]
  public string Url { get; set; }
}
