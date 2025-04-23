using System.ComponentModel.DataAnnotations;

namespace FitBackend;

#pragma warning disable CS8618 // Dto classes
public record CharacteristicsCollectionReadDto
{
  [Required]
  public ICollection<HistoricFinancialCharacteristicReadDto> HistoricFinancialCharacteristics { get; set; }

  [Required]
  public ICollection<HistoricNumberCharacteristicReadDto> HistoricNumberCharacteristics { get; set; }

  [Required]
  public ICollection<NumberCharacteristicReadDto> NumberCharacteristics { get; set; }

  [Required]
  public ICollection<TextCharacteristicReadDto> TextCharacteristics { get; set; }
}
