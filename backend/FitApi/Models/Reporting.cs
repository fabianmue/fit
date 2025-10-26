using System.ComponentModel.DataAnnotations;

namespace FIT.FitApi;

public class Reporting : Entity
{
    [Required]
    public required DateOnly PeriodStart { get; set; }

    [Required]
    public required DateOnly PeriodEnd { get; set; }

    public string? Comment { get; set; }

    [Range(0, int.MaxValue)]
    public required int Revenue { get; set; }

    public required int Earnings { get; set; }

    public required double EarningsPerShare { get; set; }

    [Range(0, int.MaxValue)]
    public required int TotalAssets { get; set; }

    [Range(0, int.MaxValue)]
    public required int TotalLiabilities { get; set; }

    // relations
    public int CompanyId { get; set; }

    public required Company Company { get; set; }
}
