using System.ComponentModel.DataAnnotations;

namespace FitBackend;

public class HistoricFinancialCharacteristic : Entity
{
  [Required]
  public required string Label { get; set; }

  [Required]
  public required string Color { get; set; }

  // relationships
  public ICollection<CompanyHistoricFinancialCharacteristic> CompanyHistoricFinancialCharacteristics { get; set; } =
    [];
}

#pragma warning disable CS8618 // Dto classes
public record HistoricFinancialCharacteristicReadDto : EntityReadDto
{
  [Required]
  public string Label { get; set; }

  [Required]
  public string Color { get; set; }

  [Required]
  public int AssociatedCompanyCharacteristicCount { get; set; }
}

public record HistoricFinancialCharacteristicCreateDto
{
  [Required]
  public string Label { get; set; }

  [Required]
  public string Color { get; set; }
}

public record HistoricFinancialCharacteristicUpdateDto
{
  [Required]
  public string Label { get; set; }

  [Required]
  public string Color { get; set; }
}
