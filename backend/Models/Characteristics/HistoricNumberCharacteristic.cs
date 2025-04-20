using System.ComponentModel.DataAnnotations;

namespace FitBackend;

public class HistoricNumberCharacteristic : Entity
{
  [Required]
  public required string Label { get; set; }

  [Required]
  public required string Color { get; set; }

  public string? Unit { get; set; }

  // relationships
  public ICollection<CompanyHistoricNumberCharacteristic> CompanyHistoricNumberCharacteristics { get; set; } =
    [];
}

#pragma warning disable CS8618 // Dto classes
public record HistoricNumberCharacteristicReadDto : EntityReadDto
{
  public string Label { get; set; }

  public string Color { get; set; }

  public string? Unit { get; set; }

  public int AssociatedCompanyCharacteristicCount { get; set; }
}

public record HistoricNumberCharacteristicCreateDto
{
  [Required]
  public string Label { get; set; }

  [Required]
  public string Color { get; set; }

  public string? Unit { get; set; }
}

public record HistoricNumberCharacteristicUpdateDto
{
  [Required]
  public string Label { get; set; }

  [Required]
  public string Color { get; set; }

  public string? Unit { get; set; }
}
