using System.ComponentModel.DataAnnotations;

namespace FitBackend;

public class CompanyTextCharacteristic : Entity
{
  [Required]
  public required string Value { get; set; }

  // relationships
  public Guid CompanyId { get; set; }

  public Company Company { get; set; } = null!;

  public Guid TextCharacteristicId { get; set; }

  public TextCharacteristic TextCharacteristic { get; set; } = null!;
}

#pragma warning disable CS8618 // Dto classes
public record CompanyTextCharacteristicReadDto : EntityReadDto
{
  public string Label { get; set; }

  public string Color { get; set; }

  public string Value { get; set; }

  public Guid TextCharacteristicId { get; set; }
}

public record CompanyTextCharacteristicCreateDto
{
  [Required]
  public string Value { get; set; }

  [Required]
  public Guid CompanyId { get; set; }

  [Required]
  public Guid TextCharacteristicId { get; set; }
}

public record CompanyTextCharacteristicUpdateDto
{
  [Required]
  public string Value { get; set; }
}
