using System.ComponentModel.DataAnnotations;

namespace FIT.FitApi;

public class Dividend : Entity
{
    [Required]
    public required DateOnly PeriodStart { get; set; }

    [Required]
    public required DateOnly PeriodEnd { get; set; }

    [Required]
    public required DateOnly PayoutDate { get; set; }

    [Required]
    public required double AmountPerShare { get; set; }

    // relations
    public int CompanyId { get; set; }

    public required Company Company { get; set; }
}
