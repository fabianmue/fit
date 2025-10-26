using System.ComponentModel.DataAnnotations;

namespace FIT.FitApi;

public class ReportingChangeDto
{
    [Required]
    public DateOnly PeriodStart { get; set; }

    [Required]
    public DateOnly PeriodEnd { get; set; }

    public string? Comment { get; set; }

    [Range(0, int.MaxValue)]
    public int Revenue { get; set; }

    public int Earnings { get; set; }

    public double EarningsPerShare { get; set; }

    [Range(0, int.MaxValue)]
    public int TotalAssets { get; set; }

    [Range(0, int.MaxValue)]
    public int TotalLiabilities { get; set; }
}
