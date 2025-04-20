using System.ComponentModel.DataAnnotations;

namespace FitBackend;

public class CompanyHistoricCurrencyCharacteristic : Entity
{
  [Required]
  public ICollection<HistoricValue> Values { get; set; } = [];

  [Required]
  public Currency Currency { get; set; }

  // relationships
  public Guid CompanyId { get; set; }

  public Company Company { get; set; } = null!;

  public Guid HistoricCurrencyCharacteristicId { get; set; }

  public HistoricCurrencyCharacteristic HistoricCurrencyCharacteristic { get; set; } = null!;
}

#pragma warning disable CS8618 // Dto classes
public record CompanyHistoricCurrencyCharacteristicReadDto : EntityReadDto
{
  public string Label { get; set; }

  public string Color { get; set; }

  public ICollection<HistoricValueReadDto> Values { get; set; }

  public Currency Currency { get; set; }

  public Guid HistoricCurrencyCharacteristicId { get; set; }
}

public record CompanyHistoricCurrencyCharacteristicCreateDto
{
  [Required]
  public ICollection<HistoricValueCreateDto> Values { get; set; }

  [Required]
  public Currency? Currency { get; set; }

  [Required]
  public Guid CompanyId { get; set; }

  [Required]
  public Guid HistoricCurrencyCharacteristicId { get; set; }
}

public record CompanyHistoricCurrencyCharacteristicUpdateDto
{
  [Required]
  public ICollection<HistoricValueCreateDto> Values { get; set; }

  [Required]
  public Currency? Currency { get; set; }
}
