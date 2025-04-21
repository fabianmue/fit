using System.ComponentModel.DataAnnotations;

namespace FitBackend;

public class CompanyHistoricFinancialCharacteristic : Entity
{
  [Required]
  public ICollection<HistoricValue> Values { get; set; } = [];

  [Required]
  public Currency Currency { get; set; }

  // relationships
  public Guid CompanyId { get; set; }

  public Company Company { get; set; } = null!;

  public Guid HistoricFinancialCharacteristicId { get; set; }

  public HistoricFinancialCharacteristic HistoricFinancialCharacteristic { get; set; } = null!;
}

#pragma warning disable CS8618 // Dto classes
public record CompanyHistoricFinancialCharacteristicReadDto : EntityReadDto
{
  public string Label { get; set; }

  public string Color { get; set; }

  public ICollection<HistoricValueReadDto> Values { get; set; }

  public Currency Currency { get; set; }

  public Guid HistoricFinancialCharacteristicId { get; set; }
}

public record CompanyHistoricFinancialCharacteristicCreateDto
{
  [Required]
  public ICollection<HistoricValueCreateDto> Values { get; set; }

  [Required]
  public Currency? Currency { get; set; }

  [Required]
  public Guid CompanyId { get; set; }

  [Required]
  public Guid HistoricFinancialCharacteristicId { get; set; }
}

public record CompanyHistoricFinancialCharacteristicUpdateDto
{
  [Required]
  public ICollection<HistoricValueCreateDto> Values { get; set; }

  [Required]
  public Currency? Currency { get; set; }
}
