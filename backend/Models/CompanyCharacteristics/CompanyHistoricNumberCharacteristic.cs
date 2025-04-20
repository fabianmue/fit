using System.ComponentModel.DataAnnotations;

namespace FitBackend;

public class CompanyHistoricNumberCharacteristic : Entity
{
  [Required]
  public ICollection<HistoricValue> Values { get; set; } = [];

  // relationships
  public Guid CompanyId { get; set; }

  public Company Company { get; set; } = null!;

  public Guid HistoricNumberCharacteristicId { get; set; }

  public HistoricNumberCharacteristic HistoricNumberCharacteristic { get; set; } = null!;
}

#pragma warning disable CS8618 // Dto classes
public record CompanyHistoricNumberCharacteristicReadDto : EntityReadDto
{
  public string Label { get; set; }

  public string Color { get; set; }

  public string? Unit { get; set; }

  public ICollection<HistoricValueReadDto> Values { get; set; }

  public Guid HistoricNumberCharacteristicId { get; set; }
}

public record CompanyHistoricNumberCharacteristicCreateDto
{
  [Required]
  public ICollection<HistoricValueCreateDto> Values { get; set; }

  [Required]
  public Guid CompanyId { get; set; }

  [Required]
  public Guid HistoricNumberCharacteristicId { get; set; }
}

public record CompanyHistoricNumberCharacteristicUpdateDto
{
  [Required]
  public ICollection<HistoricValueCreateDto> Values { get; set; }
}
