using System.ComponentModel.DataAnnotations;

namespace FitBackend;

public class HistoricCurrencyCharacteristic : Entity
{
  [Required]
  public required string Label { get; set; }

  [Required]
  public required string Color { get; set; }

  // relationships
  public ICollection<CompanyHistoricCurrencyCharacteristic> CompanyHistoricCurrencyCharacteristics { get; set; } =
    [];
}

#pragma warning disable CS8618 // Dto classes
public record HistoricCurrencyCharacteristicReadDto : EntityReadDto
{
  public string Label { get; set; }

  public string Color { get; set; }

  public int AssociatedCompanyCharacteristicCount { get; set; }
}

public record HistoricCurrencyCharacteristicCreateDto
{
  [Required]
  public string Label { get; set; }

  [Required]
  public string Color { get; set; }
}

public record HistoricCurrencyCharacteristicUpdateDto
{
  [Required]
  public string Label { get; set; }

  [Required]
  public string Color { get; set; }
}
