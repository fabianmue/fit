using System.ComponentModel.DataAnnotations;

namespace FIT.FitApi;

public class Company : Entity
{
    [Required]
    public required string Name { get; set; }

    [Required]
    [AllowedValues(1000, 1000000)]
    public required int ReportingMultiplier { get; set; }

    [Required]
    [AllowedValues("CHF", "USD", "EUR")]
    public required string ReportingCurrency { get; set; }

    [Required]
    [AllowedValues("CHF", "USD", "EUR")]
    public required string ShareCurrency { get; set; }

    [Required]
    [AllowedValues("CHF", "USD", "EUR")]
    public required string DividendCurrency { get; set; }

    // relations
    public List<Reporting> Reportings { get; set; } = [];

    public List<SharePrice> SharePrices { get; set; } = [];

    public List<Dividend> Dividends { get; set; } = [];
}
