using System.ComponentModel.DataAnnotations;

namespace FitBackend;

public class HistoricValue : Entity
{
  [Required]
  public required float Value { get; set; }

  [Required]
  public required DateTime Date { get; set; }

  // relationships
  public Guid? CompanyHistoricNumberCharacteristicId { get; set; }

  public CompanyHistoricNumberCharacteristic? CompanyHistoricNumberCharacteristic { get; set; }

  public Guid? CompanyHistoricFinancialCharacteristicId { get; set; }

  public CompanyHistoricFinancialCharacteristic? CompanyHistoricFinancialCharacteristic { get; set; }
}

public record HistoricValueReadDto
{
  [Required]
  public float Value { get; set; }

  [Required]
  public DateTime Date { get; set; }
}

public record HistoricValueCreateDto
{
  [Required]
  public float Value { get; set; }

  [Required]
  public DateTime Date { get; set; }
}
