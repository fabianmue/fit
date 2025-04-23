using System.ComponentModel.DataAnnotations;

namespace FitBackend;

public class TextCharacteristic : Entity
{
  [Required]
  public required string Label { get; set; }

  [Required]
  public required string Color { get; set; }

  // relationships
  public ICollection<CompanyTextCharacteristic> CompanyTextCharacteristics { get; set; } = [];
}

#pragma warning disable CS8618 // Dto classes
public record TextCharacteristicReadDto : EntityReadDto
{
  [Required]
  public string Label { get; set; }

  [Required]
  public string Color { get; set; }

  [Required]
  public int AssociatedCompanyCharacteristicCount { get; set; }
}

public record TextCharacteristicCreateDto
{
  [Required]
  public string Label { get; set; }

  [Required]
  public string Color { get; set; }
}

public record TextCharacteristicUpdateDto
{
  [Required]
  public string Label { get; set; }

  [Required]
  public string Color { get; set; }
}
