using System.ComponentModel.DataAnnotations;

namespace FitBackend;

public class Characteristic : Entity
{
  [Required]
  public required string Icon { get; set; }

  [Required]
  public required string Color { get; set; }

  [Required]
  public required string Label { get; set; }

  public ICollection<CompanyCharacteristic> CompanyCharacteristics { get; set; } = [];
}

#pragma warning disable CS8618 // Dto classes
public class CharacteristicReadDto : EntityReadDto
{
  public string Icon { get; set; }

  public string Color { get; set; }

  public string Label { get; set; }

  public int AssociatedCompanyCharacteristicCount { get; set; }
}

public class CharacteristicCreateDto
{
  public required string Icon { get; set; }

  public required string Color { get; set; }

  public required string Label { get; set; }
}

public class CharacteristicUpdateDto
{
  public required string Icon { get; set; }

  public required string Color { get; set; }

  public required string Label { get; set; }
}
