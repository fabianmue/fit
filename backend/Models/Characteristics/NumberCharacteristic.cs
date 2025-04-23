using System.ComponentModel.DataAnnotations;

namespace FitBackend;

public class NumberCharacteristic : Entity
{
  [Required]
  public required string Label { get; set; }

  [Required]
  public required string Color { get; set; }

  public string? Unit { get; set; }

  // relationships
  public ICollection<CompanyNumberCharacteristic> CompanyNumberCharacteristics { get; set; } = [];
}

#pragma warning disable CS8618 // Dto classes
public record NumberCharacteristicReadDto : EntityReadDto
{
  [Required]
  public string Label { get; set; }

  [Required]
  public string Color { get; set; }

  public string? Unit { get; set; }

  [Required]
  public int AssociatedCompanyCharacteristicCount { get; set; }
}

public record NumberCharacteristicCreateDto
{
  [Required]
  public string Label { get; set; }

  [Required]
  public string Color { get; set; }

  public string? Unit { get; set; }
}

public record NumberCharacteristicUpdateDto
{
  [Required]
  public string Label { get; set; }

  [Required]
  public string Color { get; set; }

  public string? Unit { get; set; }
}
