using System.ComponentModel.DataAnnotations;

namespace FIT.FitApi;

public class CompanyDto : EntityDto
{
    [Required]
    public string Name { get; set; }

    public string? Story { get; set; }

    public DateOnly? NextReportingDate { get; set; }

    public int ReportingMultiplier { get; set; }

    [Required]
    public string ReportingCurrency { get; set; }

    [Required]
    public string ShareCurrency { get; set; }

    [Required]
    public string ShareIsin { get; set; }

    [Required]
    public string ShareSymbol { get; set; }

    public DateOnly? NextDividendRecordingDate { get; set; }

    [Required]
    public string DividendCurrency { get; set; }

    // relations
    public List<ReportingDto>? Reportings { get; set; }

    public List<SharePriceDto>? SharePrices { get; set; }

    public List<DividendDto>? Dividends { get; set; }
}
