using System.ComponentModel.DataAnnotations;

namespace FIT.FitApi;

public class Reporting : Entity
{
    [Required]
    public required DateOnly PeriodStart { get; set; }

    [Required]
    public required DateOnly PeriodEnd { get; set; }

    [Range(0, int.MaxValue)]
    public required int Revenue { get; set; }

    [Range(0, int.MaxValue)]
    public required int Earnings { get; set; }

    [Range(0, double.MaxValue)]
    public required double EarningsPerShare { get; set; }

    [Range(0, int.MaxValue)]
    public required int TotalAssets { get; set; }

    [Range(0, int.MaxValue)]
    public required int TotalLiabilities { get; set; }

    // relations
    public int CompanyId { get; set; }

    public required Company Company { get; set; }
}
