using System.ComponentModel.DataAnnotations;

namespace FitBackend;

public class CompanyNumberCharacteristic : Entity
{
  [Required]
  public required float Value { get; set; }

  // relationships
  public Guid CompanyId { get; set; }

  public Company Company { get; set; } = null!;

  public Guid NumberCharacteristicId { get; set; }

  public NumberCharacteristic NumberCharacteristic { get; set; } = null!;
}

#pragma warning disable CS8618 // Dto classes
public record CompanyNumberCharacteristicReadDto : EntityReadDto
{
  [Required]
  public string Label { get; set; }

  [Required]
  public string Color { get; set; }

  public string? Unit { get; set; }

  [Required]
  public float Value { get; set; }

  [Required]
  public Guid NumberCharacteristicId { get; set; }
}

public record CompanyNumberCharacteristicCreateDto
{
  [Required]
  public float Value { get; set; }

  [Required]
  public Guid CompanyId { get; set; }

  [Required]
  public Guid NumberCharacteristicId { get; set; }
}

public record CompanyNumberCharacteristicUpdateDto
{
  [Required]
  public float Value { get; set; }
}
