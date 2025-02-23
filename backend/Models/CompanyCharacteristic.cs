using System.ComponentModel.DataAnnotations;

namespace FitBackend;

public class CompanyCharacteristic : Entity
{
  [Required]
  public required float Value { get; set; }

  public string? Unit { get; set; }

  [Required]
  public required Guid CompanyId { get; set; }

  public Company Company { get; set; } = null!;

  [Required]
  public required Guid CharacteristicId { get; set; }

  public Characteristic Characteristic { get; set; } = null!;
}

#pragma warning disable CS8618 // Dto classes
public class CompanyCharacteristicReadDto : EntityReadDto
{
  public string Icon { get; set; }

  public string Color { get; set; }

  public string Label { get; set; }

  public float Value { get; set; }

  public string? Unit { get; set; }

  public string CharacteristicId { get; set; }
}

public class CompanyCharacteristicCreateDto
{
  public required float Value { get; set; }

  public string? Unit { get; set; }

  public Guid CompanyId { get; set; }

  public Guid CharacteristicId { get; set; }
}

public class CompanyCharacteristicUpdateDto
{
  public required float Value { get; set; }

  public string? Unit { get; set; }
}
