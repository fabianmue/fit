using System.ComponentModel.DataAnnotations;

namespace FIT.FitApi;

public class ReportingDto : EntityDto
{
    [Required]
    public DateOnly PeriodStart { get; set; }

    [Required]
    public DateOnly PeriodEnd { get; set; }

    public string? Comment { get; set; }

    public int Revenue { get; set; }

    public int Earnings { get; set; }

    public double EarningsPerShare { get; set; }

    public int TotalAssets { get; set; }

    public int TotalLiabilities { get; set; }
}
