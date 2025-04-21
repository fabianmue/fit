using System.ComponentModel.DataAnnotations;

namespace FitBackend;

public class Company : Entity
{
  [Required]
  public required string Name { get; set; }

  [Required]
  public required StockExchange StockExchange { get; set; }

  [Required]
  public required Currency StockCurrency { get; set; }

  [Required]
  public required string StockCode { get; set; }

  [Required]
  public required Currency FinancialReportingCurrency { get; set; }

  [Required]
  public required FinancialReportingInterval FinancialReportingInterval { get; set; }

  [Required]
  public required Multiplier FinancialReportingMultiplier { get; set; }

  public string[] FinancialReportingSourceUrls { get; set; } = [];

  public string? Comment { get; set; }

  public string? LogoUrl { get; set; }

  // relationships
  public ICollection<CompanyTextCharacteristic> CompanyTextCharacteristics { get; set; } = [];

  public ICollection<CompanyNumberCharacteristic> CompanyNumberCharacteristics { get; set; } = [];

  public ICollection<CompanyHistoricNumberCharacteristic> CompanyHistoricNumberCharacteristics { get; set; } =
    [];

  public ICollection<CompanyHistoricFinancialCharacteristic> CompanyHistoricFinancialCharacteristics { get; set; } =
    [];
}

#pragma warning disable CS8618 // Dto classes
public record CompanyReadDto : EntityReadDto
{
  public string Name { get; set; }

  public StockExchange StockExchange { get; set; }

  public Currency StockCurrency { get; set; }

  public string StockCode { get; set; }

  public Currency FinancialReportingCurrency { get; set; }

  public FinancialReportingInterval FinancialReportingInterval { get; set; }

  public Multiplier FinancialReportingMultiplier { get; set; }

  public ICollection<string> FinancialReportingSourceUrls { get; set; }

  public string? Comment { get; set; }

  public string? LogoUrl { get; set; }

  public ICollection<CompanyTextCharacteristicReadDto> CompanyTextCharacteristics { get; set; } =
    [];

  public ICollection<CompanyNumberCharacteristicReadDto> CompanyNumberCharacteristics { get; set; } =
    [];

  public ICollection<CompanyHistoricNumberCharacteristicReadDto> CompanyHistoricNumberCharacteristics { get; set; } =
    [];

  public ICollection<CompanyHistoricFinancialCharacteristicReadDto> CompanyHistoricFinancialCharacteristics { get; set; } =
    [];
}

public record CompanyCreateDto
{
  [Required]
  public string Name { get; set; }

  [Required]
  public StockExchange? StockExchange { get; set; }

  [Required]
  public string StockCode { get; set; }

  [Required]
  public Currency? FinancialReportingCurrency { get; set; }

  [Required]
  public FinancialReportingInterval? FinancialReportingInterval { get; set; }

  [Required]
  public Multiplier? FinancialReportingMultiplier { get; set; }

  [Required]
  public ICollection<string> FinancialReportingSourceUrls { get; set; }

  public string? Comment { get; set; }

  public string? LogoUrl { get; set; }
}

public record CompanyUpdateDto
{
  [Required]
  public string Name { get; set; }

  [Required]
  public StockExchange? StockExchange { get; set; }

  [Required]
  public string StockCode { get; set; }

  [Required]
  public Currency? FinancialReportingCurrency { get; set; }

  [Required]
  public FinancialReportingInterval? FinancialReportingInterval { get; set; }

  [Required]
  public Multiplier? FinancialReportingMultiplier { get; set; }

  [Required]
  public ICollection<string> FinancialReportingSourceUrls { get; set; }

  public string? Comment { get; set; }

  public string? LogoUrl { get; set; }
}
