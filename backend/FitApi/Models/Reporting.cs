using System.ComponentModel.DataAnnotations;

namespace FIT.FitApi;

public class Reporting : Entity
{
    [Required]
    public required DateOnly PeriodStart { get; set; }

    [Required]
    public required DateOnly PeriodEnd { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public required int NetSales { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public required int NetIncome { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public required int OutstandingShares { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public required int TotalAssets { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public required int TotalLiabilities { get; set; }

    // relations
    public int CompanyId { get; set; }

    public required Company Company { get; set; }
}
