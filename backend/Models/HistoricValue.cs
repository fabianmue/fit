using System.ComponentModel.DataAnnotations;

namespace FitBackend;

public class HistoricValue : Entity
{
  [Required]
  public required float Value { get; set; }

  [Required]
  public required DateTime Date { get; set; }

  [Required]
  public required Guid CompanyHistoricCharacteristicId { get; set; }

  public CompanyHistoricCharacteristic CompanyHistoricCharacteristic { get; set; } = null!;
}

public class HistoricValueReadDto : EntityReadDto
{
  public float Value { get; set; }

  public DateTime Date { get; set; }
}

public class HistoricValueCreateDto
{
  public required float Value { get; set; }

  public required DateTime Date { get; set; }
}
