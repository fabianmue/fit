using System.ComponentModel.DataAnnotations;

namespace FitBackend;

public class Company : Entity
{
  [Required]
  public required string Name { get; set; }

  [Required]
  public required string LogoUrl { get; set; }

  // relationships
  public ICollection<CompanyCharacteristic> CompanyCharacteristics { get; set; } = [];

  public ICollection<CompanyHistoricCharacteristic> CompanyHistoricCharacteristics { get; set; } =
    [];
}

#pragma warning disable CS8618 // Dto classes
public class CompanyReadDto : EntityReadDto
{
  public string Name { get; set; }

  public string LogoUrl { get; set; }

  public ICollection<CompanyCharacteristicReadDto> CompanyCharacteristics { get; set; }

  public ICollection<CompanyHistoricCharacteristicReadDto> CompanyHistoricCharacteristics { get; set; } =
    [];
}

public class CompanyCreateDto
{
  public required string Name { get; set; }

  public required string LogoUrl { get; set; }
}

public class CompanyUpdateDto
{
  public required string Name { get; set; }

  public required string LogoUrl { get; set; }
}
