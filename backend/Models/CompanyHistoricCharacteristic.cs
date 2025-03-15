using System.ComponentModel.DataAnnotations;

namespace FitBackend;

public class CompanyHistoricCharacteristic : Entity
{
  public ICollection<HistoricValue> Values { get; set; } = [];

  public string? Unit { get; set; }

  // relationships
  public Guid CompanyId { get; set; }

  public Company Company { get; set; } = null!;

  public Guid HistoricCharacteristicId { get; set; }

  public HistoricCharacteristic HistoricCharacteristic { get; set; } = null!;
}

#pragma warning disable CS8618 // Dto classes
public class CompanyHistoricCharacteristicReadDto : EntityReadDto
{
  public CharacteristicType Type { get; set; }

  public string Label { get; set; }

  public string Color { get; set; }

  public ICollection<HistoricValueReadDto> Values { get; set; }

  public string? Unit { get; set; }

  public string HistoricCharacteristicId { get; set; }
}

public class CompanyHistoricCharacteristicCreateDto
{
  public required ICollection<HistoricValueCreateDto> Values { get; set; }

  public string? Unit { get; set; }

  public required Guid CompanyId { get; set; }

  public required Guid HistoricCharacteristicId { get; set; }
}

public class CompanyHistoricCharacteristicUpdateDto
{
  public required ICollection<HistoricValueCreateDto> Values { get; set; }

  public string? Unit { get; set; }
}
