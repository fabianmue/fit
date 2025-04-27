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

  public string? Comment { get; set; }

  public string? LogoUrl { get; set; }

  // relationships
  public ICollection<Link> Links { get; set; } = [];

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
  [Required]
  public string Name { get; set; }

  [Required]
  public StockExchange StockExchange { get; set; }

  [Required]
  public Currency StockCurrency { get; set; }

  [Required]
  public string StockCode { get; set; }

  [Required]
  public Currency FinancialReportingCurrency { get; set; }

  [Required]
  public FinancialReportingInterval FinancialReportingInterval { get; set; }

  [Required]
  public Multiplier FinancialReportingMultiplier { get; set; }

  public string? Comment { get; set; }

  public string? LogoUrl { get; set; }

  [Required]
  public ICollection<LinkReadDto> Links { get; set; }

  [Required]
  public ICollection<CompanyTextCharacteristicReadDto> CompanyTextCharacteristics { get; set; }

  [Required]
  public ICollection<CompanyNumberCharacteristicReadDto> CompanyNumberCharacteristics { get; set; }

  [Required]
  public ICollection<CompanyHistoricNumberCharacteristicReadDto> CompanyHistoricNumberCharacteristics { get; set; }

  [Required]
  public ICollection<CompanyHistoricFinancialCharacteristicReadDto> CompanyHistoricFinancialCharacteristics { get; set; }
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

  public string? Comment { get; set; }

  public string? LogoUrl { get; set; }

  [Required]
  public ICollection<LinkCreateDto> Links { get; set; }
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

  public string? Comment { get; set; }

  public string? LogoUrl { get; set; }

  [Required]
  public ICollection<LinkCreateDto> Links { get; set; }
}
