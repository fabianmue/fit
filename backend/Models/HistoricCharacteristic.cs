using System.ComponentModel.DataAnnotations;

namespace FitBackend;

public class HistoricCharacteristic : Entity
{
  [Required]
  public required CharacteristicType Type { get; set; }

  [Required]
  public required string Label { get; set; }

  [Required]
  public required string Color { get; set; }

  public ICollection<CompanyHistoricCharacteristic> CompanyHistoricCharacteristics { get; set; } =
    [];
}

#pragma warning disable CS8618 // Dto classes
public class HistoricCharacteristicReadDto : EntityReadDto
{
  public CharacteristicType Type { get; set; }

  public string Label { get; set; }

  public string Color { get; set; }

  public int AssociatedCompanyHistoricCharacteristicCount { get; set; }
}

public class HistoricCharacteristicCreateDto
{
  public required CharacteristicType Type { get; set; }

  public required string Label { get; set; }

  public required string Color { get; set; }
}

public class HistoricCharacteristicUpdateDto
{
  public required CharacteristicType Type { get; set; }

  public required string Label { get; set; }

  public required string Color { get; set; }
}
