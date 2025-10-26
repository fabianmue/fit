using System.ComponentModel.DataAnnotations;

namespace FIT.FitApi;

public class CompanyChangeDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public DateOnly NextReportingDate { get; set; }

    [AllowedValues(1000, 1000000)]
    public int ReportingMultiplier { get; set; }

    [Required]
    [AllowedValues("CHF", "USD", "EUR")]
    public string ReportingCurrency { get; set; }

    [Required]
    [AllowedValues("CHF", "USD", "EUR")]
    public string ShareCurrency { get; set; }

    [Required]
    public string ShareIsin { get; set; }

    [Required]
    public string ShareSymbol { get; set; }

    [Required]
    [AllowedValues("CHF", "USD", "EUR")]
    public string DividendCurrency { get; set; }
}
