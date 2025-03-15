using System.ComponentModel.DataAnnotations;

namespace FitBackend;

public class Characteristic : Entity
{
  [Required]
  public required CharacteristicType Type { get; set; }

  [Required]
  public required string Label { get; set; }

  [Required]
  public required string Color { get; set; }

  // relationships
  public ICollection<CompanyCharacteristic> CompanyCharacteristics { get; set; } = [];
}

#pragma warning disable CS8618 // Dto classes
public class CharacteristicReadDto : EntityReadDto
{
  public CharacteristicType Type { get; set; }

  public string Label { get; set; }

  public string Color { get; set; }

  public int AssociatedCompanyCharacteristicCount { get; set; }
}

public class CharacteristicCreateDto
{
  public required CharacteristicType Type { get; set; }

  public required string Label { get; set; }

  public required string Color { get; set; }
}

public class CharacteristicUpdateDto
{
  public required CharacteristicType Type { get; set; }

  public required string Label { get; set; }

  public required string Color { get; set; }
}
