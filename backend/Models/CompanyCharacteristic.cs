using System.ComponentModel.DataAnnotations;

namespace FitBackend;

public class CompanyCharacteristic : Entity
{
  [Required]
  public required float Value { get; set; }

  public string? Unit { get; set; }

  // relationships
  public Guid CompanyId { get; set; }

  public Company Company { get; set; } = null!;

  public Guid CharacteristicId { get; set; }

  public Characteristic Characteristic { get; set; } = null!;
}

#pragma warning disable CS8618 // Dto classes
public class CompanyCharacteristicReadDto : EntityReadDto
{
  public CharacteristicType Type { get; set; }

  public string Label { get; set; }

  public string Color { get; set; }

  public float Value { get; set; }

  public string? Unit { get; set; }

  public string CharacteristicId { get; set; }
}

public class CompanyCharacteristicCreateDto
{
  public required float Value { get; set; }

  public string? Unit { get; set; }

  public required Guid CompanyId { get; set; }

  public required Guid CharacteristicId { get; set; }
}

public class CompanyCharacteristicUpdateDto
{
  public required float Value { get; set; }

  public string? Unit { get; set; }
}
