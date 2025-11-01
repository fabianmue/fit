using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FIT.FitApi;

[Index(nameof(Name), IsUnique = true)]
public class Company : Entity
{
    [Required]
    public required string Name { get; set; }

    public string? Story { get; set; }

    public DateOnly? NextReportingDate { get; set; }

    [AllowedValues(1000, 1000000)]
    public int ReportingMultiplier { get; set; }

    [Required]
    [AllowedValues("CHF", "USD", "EUR")]
    public required string ReportingCurrency { get; set; }

    [Required]
    [AllowedValues("CHF", "USD", "EUR")]
    public required string ShareCurrency { get; set; }

    [Required]
    public required string ShareIsin { get; set; }

    [Required]
    public required string ShareSymbol { get; set; }

    public DateOnly? NextDividendRecordingDate { get; set; }

    [Required]
    [AllowedValues("CHF", "USD", "EUR")]
    public required string DividendCurrency { get; set; }

    // relations
    public List<Reporting> Reportings { get; set; } = [];

    public List<SharePrice> SharePrices { get; set; } = [];

    public List<Dividend> Dividends { get; set; } = [];
}
