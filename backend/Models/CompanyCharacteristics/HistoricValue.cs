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

  public Guid? CompanyHistoricCurrencyCharacteristicId { get; set; }

  public CompanyHistoricCurrencyCharacteristic? CompanyHistoricCurrencyCharacteristic { get; set; }
}

public record HistoricValueReadDto
{
  public float Value { get; set; }

  public DateTime Date { get; set; }
}

public record HistoricValueCreateDto
{
  [Required]
  public float Value { get; set; }

  [Required]
  public DateTime Date { get; set; }
}
